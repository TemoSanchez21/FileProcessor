using File = FileProcessor.Domain.Models.File;
using FileProcessor.Domain.Repositories;
using FileProcessor.Infrastructure.Persistence;

namespace FileProcessor.Infrastructure.Repositories;

internal class FileRepository(GlobalDbContext dbContext) : IFileRepository
{
    public async Task<Guid> SaveFileAsync(File file)
    {
        await dbContext.Files.AddAsync(file);
        await dbContext.SaveChangesAsync();
        return file.Id;
    }

    public async Task<Guid> SaveFolderFilesAsync(IEnumerable<File> files)
    {
        await dbContext.Files.AddRangeAsync(files);
        await dbContext.SaveChangesAsync();
        return files.FirstOrDefault()!.Folder.Id;
    }
}
