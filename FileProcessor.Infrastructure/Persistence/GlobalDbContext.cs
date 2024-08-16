
using FileProcessor.Domain.Models;
using Microsoft.EntityFrameworkCore;
using File = FileProcessor.Domain.Models.File;

namespace FileProcessor.Infrastructure.Persistence;

public class GlobalDbContext(DbContextOptions options) : DbContext(options)
{
    internal DbSet<File> Files { get; set; }
    internal DbSet<Folder> Folders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<File>()
            .HasOne(file => file.Folder)
            .WithMany(folder => folder.Files)
            .HasForeignKey(file => file.FolderId);
    }
}
