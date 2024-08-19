

namespace FileProcessor.Domain.Services;

public interface IFileTransferService
{
    public Task<bool> TransferFile(Stream file, string fileName);
}
