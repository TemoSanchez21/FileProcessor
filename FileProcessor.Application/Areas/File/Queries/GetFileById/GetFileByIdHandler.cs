using FileProcessor.Domain.Repositories;
using FileProcessor.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FileProcessor.Application.Areas.File.Queries.GetFileById;

public class GetFileByIdHandler(
    ILogger<GetFileByIdHandler> logger,
    IFileTransferService fileTransferService,
    IFileRepository fileRepository
) : IRequestHandler<GetFileByIdQuery, (Stream, string)>
{
    public async Task<(Stream, string)> Handle(GetFileByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Return file with id {FileId}", request.Id);
        var fileEntity = await fileRepository.GetFileByIdAsync(request.Id);

        if (fileEntity == null)
            throw new FileNotFoundException("The file was not found");

        var fileStream = await fileTransferService.DownloadFileAsync(fileEntity.RemotePath);


        return (fileStream, fileEntity.FileName);
    }
}
