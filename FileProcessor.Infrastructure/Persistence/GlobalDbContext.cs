
using FileProcessor.Domain.Models;
using Microsoft.EntityFrameworkCore;
using FileModel = FileProcessor.Domain.Models.File;

namespace FileProcessor.Infrastructure.Persistence;

public class GlobalDbContext(DbContextOptions options) : DbContext(options)
{
    internal DbSet<FileModel> Files { get; set; }
    internal DbSet<Folder> Folders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FileModel>()
            .Property(e => e.FileName).IsRequired();


        modelBuilder.Entity<FileModel>()
            .Property(e => e.RemotePath).IsRequired();


        modelBuilder.Entity<FileModel>()
            .Property(e => e.ProviderPassword).IsRequired();


        modelBuilder.Entity<Folder>()
            .Property(e => e.Name).IsRequired();


        modelBuilder.Entity<Folder>()
            .Property(e => e.ClientName).IsRequired();

        modelBuilder.Entity<FileModel>()
            .HasOne(file => file.Folder)
            .WithMany(folder => folder.Files)
            .HasForeignKey(file => file.FolderId);
    }
}
