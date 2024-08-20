using System.ComponentModel.DataAnnotations;

namespace FileProcessor.Domain.Models;

public class File
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string FileName = string.Empty;

    [Required]
    public Guid FolderId { get; set; }

    [Required]
    public Folder Folder { get; set; } = new Folder();
    
    [Required]
    public string RemotePath = string.Empty;

    [Required]
    public string ProviderPassword = string.Empty;

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }
}
