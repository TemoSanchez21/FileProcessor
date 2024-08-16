namespace FileProcessor.Domain.Models;

public class File
{
    public Guid Id { get; set; }
    public string FileName = string.Empty;

    public Guid FolderId { get; set; }
    public Folder Folder { get; set; } = new Folder();
    
    public string ProviderPassword = string.Empty;

    public DateOnly CreatedAt { get; set; }
    public DateOnly UpdatedAt { get; set; }
}
