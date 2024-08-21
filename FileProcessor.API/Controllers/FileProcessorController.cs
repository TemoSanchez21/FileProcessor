using FileProcessor.Application.Areas.File.Commands.UploadCompressedFolder;
using FileProcessor.Application.Areas.File.Queries.GetFileById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FileProcessor.API.Controllers;

[ApiController]
[Route("[controller]")]
public class FileProcessorController (IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UploadCompressedFolder(IFormFile file, string clientName)
    {
        var folderId = await mediator.Send(new UploadCompressedFolderCommand { ClientName = clientName, CompressedFileStream = file.OpenReadStream()});   
        return Ok(folderId);
    }

    [HttpGet]
    public async Task<IActionResult> DownloadFile(Guid fileId)
    {
        var (fileStream, fileName) = await mediator.Send(new GetFileByIdQuery { Id = fileId });
        return File(fileStream, "application/octet-stream", fileName);
    }

}
