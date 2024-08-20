using System.ComponentModel.DataAnnotations;

namespace FileProcessor.Domain.Models;

public class Folder
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string ClientName = string.Empty;

    [Required]
    public List<File> Files { get; set; } = new List<File>();

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }
}
