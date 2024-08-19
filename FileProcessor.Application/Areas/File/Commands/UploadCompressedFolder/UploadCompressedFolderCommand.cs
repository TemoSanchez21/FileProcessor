using MediatR;

namespace FileProcessor.Application.Areas.File.Commands.UploadCompressedFolder;

public class UploadCompressedFolderCommand : IRequest<Guid>
{
    public string ClientName { get; set; } = string.Empty;
    public byte[] CompressedFileBytes { get; set; } = new byte[0];
}
