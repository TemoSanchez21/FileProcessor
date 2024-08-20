

namespace FileProcessor.Domain.Services;

public interface IFileTransferService
{
    public Task<bool> TransferFileAsync(Stream file, string fileName);
    public Task<Stream> DownloadFileAsync(string remotePath);
}
