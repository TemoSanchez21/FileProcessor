using FileProcessor.Domain.Models;
using FileProcessor.Domain.Repositories;
using FileProcessor.Infrastructure.Persistence;

namespace FileProcessor.Infrastructure.Repositories;

internal class FolderRepository(GlobalDbContext dbContext) : IFolderRepository
{
    public async Task<Guid> SaveFolderAsync(Folder folder)
    {
    
        await dbContext.Folders.AddAsync(folder);
        await dbContext.SaveChangesAsync();
    
        return folder.Id;
    }
}
