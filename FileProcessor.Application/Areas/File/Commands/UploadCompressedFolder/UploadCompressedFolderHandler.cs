using MediatR;
using FileProcessor.Domain.Exceptions;
using System.IO.Compression;
using SharpCompress.Archives.Tar;
using SharpCompress.Archives;
using SharpCompress.Common;

namespace FileProcessor.Application.Areas.File.Commands.UploadCompressedFolder;

public class UploadCompressedFolderHandler : IRequestHandler<UploadCompressedFolderCommand, Guid>
{
    public async Task<Guid> Handle(UploadCompressedFolderCommand command, CancellationToken cancellationToken)
    {
        if (command.CompressedFileBytes == null || command.CompressedFileBytes.Length == 0)
            throw new NotFolderException("The folder was not provided");

        try
        {
            var tarBytes = DecompressGzip(command.CompressedFileBytes);
            var outputDirectory = Path.Combine("Output", $"{command.ClientName.ToLower()}-{DateTime.Now.ToString("dd-MM")}");

            Directory.CreateDirectory(outputDirectory);
            ExtractTar(tarBytes, outputDirectory);
        }
        catch
        {
            throw new UnableToDecompressException("Folder was unable to decompress");
        }


        return Guid.NewGuid();
    }

    private static void ExtractTar(byte[] tarBytes, string outputDirectory)
    {
        using var tarStream = new MemoryStream(tarBytes);
        using var archive = TarArchive.Open(tarStream);

        foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
        {
            entry.WriteToDirectory(outputDirectory, new ExtractionOptions
            {
                ExtractFullPath = true,
                Overwrite = true,
            });
        }
    }

    private static byte[] DecompressGzip(byte[] compressedBytes) 
    {
        using var inputStream = new MemoryStream(compressedBytes);
        using var gzipStream = new GZipStream(inputStream, CompressionMode.Decompress);
        using var outputStream = new MemoryStream();

        gzipStream.CopyTo(outputStream);
        return outputStream.ToArray();
    }
}
