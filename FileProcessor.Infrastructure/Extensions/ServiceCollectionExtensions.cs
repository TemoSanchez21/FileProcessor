using FileProcessor.Infrastructure.Persistence;
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
    }
}
