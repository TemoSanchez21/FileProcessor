using FileProcessor.Domain.Repositories;
using FileProcessor.Domain.Services;
using FileProcessor.Infrastructure.Persistence;
using FileProcessor.Infrastructure.Repositories;
using FileProcessor.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileProcessor.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConnectionString = configuration.GetConnectionString("FileProcessorDb");

        services.AddDbContext<GlobalDbContext>(options => options.UseNpgsql(dbConnectionString));


        // Add repositories
        services.AddScoped<IFileRepository, FileRepository>();
        services.AddScoped<IFolderRepository, FolderRepository>();

        // Add services
        services.AddScoped<IFileTransferService, FluentFTPTransferService>();
    }
}
