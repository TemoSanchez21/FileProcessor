using FileProcessor.Application.Areas.File.Commands.UploadCompressedFolder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FileProcessor.API.Controllers;

[ApiController]
[Route("[controller]")]
public class FileProcessorController (IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UploadCompressedFolder(IFormFile file, string passFile, string clientName)
    {
        using var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);

        var command = new UploadCompressedFolderCommand
        {
            ClientName = clientName,
            CompressedFileBytes = memoryStream.ToArray()
        };

        var folderId = await mediator.Send(command);
        
        return Ok(folderId);
    }

}
