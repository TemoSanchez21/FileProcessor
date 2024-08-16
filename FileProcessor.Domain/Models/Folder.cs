namespace FileProcessor.Domain.Models;

public class Folder
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    public string ClientName = string.Empty;

    public List<File> Files { get; set; } = new List<File>();

    public DateOnly CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
