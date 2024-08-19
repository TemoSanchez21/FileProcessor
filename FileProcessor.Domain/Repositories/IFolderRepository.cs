using FileProcessor.Domain.Models;

namespace FileProcessor.Domain.Repositories;

public interface IFolderRepository
{
    public Task<Guid> SaveFolderAsync(Folder folder);
}
