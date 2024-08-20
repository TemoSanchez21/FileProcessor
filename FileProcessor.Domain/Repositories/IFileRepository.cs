using File = FileProcessor.Domain.Models.File;

namespace FileProcessor.Domain.Repositories;

public interface IFileRepository
{
    public Task<Guid> SaveFileAsync(File file);
    public Task<Guid> SaveFolderFilesAsync(IEnumerable<File> files);
    public Task<File?> GetFileByIdAsync(Guid id);
}
