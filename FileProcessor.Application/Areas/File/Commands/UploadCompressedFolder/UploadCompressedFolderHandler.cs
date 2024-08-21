using MediatR;
using FileProcessor.Domain.Exceptions;
using System.IO.Compression;
using SharpCompress.Archives.Tar;
using FileProcessor.Domain.Repositories;
using FileProcessor.Domain.Services;
using FileModel = FileProcessor.Domain.Models.File;
using FileProcessor.Domain.Models;
using Microsoft.Extensions.Logging;

namespace FileProcessor.Application.Areas.File.Commands.UploadCompressedFolder;

public class UploadCompressedFolderHandler(
    IFileRepository fileRepository,
    IFileTransferService fileTransferService,
    ILogger<UploadCompressedFolderHandler> logger
) : IRequestHandler<UploadCompressedFolderCommand, Guid>
{
    public async Task<Guid> Handle(UploadCompressedFolderCommand command, CancellationToken cancellationToken)
    {
        if (command.CompressedFileStream == null)
            throw new NotFolderException("The folder was not provided");

        logger.LogInformation("Decompressing file for client {ClientName}", command.ClientName);
        var tarBytes = DecompressGzip(command.CompressedFileStream);

        using var tarStream = new MemoryStream(tarBytes);
        using var archive = TarArchive.Open(tarStream);

        var folder = new Folder
        {
            Id = Guid.NewGuid(),
            ClientName = command.ClientName,
            Name = $"{command.ClientName.ToLower()}{DateTime.Now}".Replace(" ", ""),
            UpdatedAt = DateTime.Now.ToUniversalTime(),
            CreatedAt = DateTime.Now.ToUniversalTime()
        };

        foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
        {
            using var entryStream = entry.OpenEntryStream();
            var fileBytes = ConvertStreamToByteArray(entryStream);

            using var fileStream = new MemoryStream(fileBytes);

            logger.LogInformation("Uploading file {FileName} to FTP provider", entry.Key);
            var isSucceded = await fileTransferService.TransferFileAsync(fileStream, entry.Key ?? "Placeholder");

            if (isSucceded)
            {
                var fileRecord = new FileModel
                {
                    Id = Guid.NewGuid(),
                    FileName = entry.Key!.Split("/").Last(),
                    Folder = folder,
                    ProviderPassword = "Password",
                    RemotePath = $"/{entry.Key}" ?? "Placeholder",
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    UpdatedAt = DateTime.Now.ToUniversalTime(),
                };

                logger.LogInformation("Upload successful, saving entity for {FileName} in database", entry.Key);
                await fileRepository.SaveFileAsync(fileRecord);
            }
            else
            {
                throw new UploadFileException("Something went wrong while uploading the file");
            }
        }

        return folder.Id;
    }

    private static byte[] ConvertStreamToByteArray(Stream stream)
    {
        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }

    private static byte[] DecompressGzip(Stream compressedStream)
    {
        using var gzipStream = new GZipStream(compressedStream, CompressionMode.Decompress);
        using var outputStream = new MemoryStream();

        gzipStream.CopyTo(outputStream);
        return outputStream.ToArray();
    }
}