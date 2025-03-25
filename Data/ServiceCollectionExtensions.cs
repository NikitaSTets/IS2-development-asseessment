using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
    {
        services.AddDbContext<ExporterDbContext>(options =>
            options.UseInMemoryDatabase("ExporterDb"));

        services.AddScoped<IPolicyRepository, PolicyRepository>();

        return services;
    }
}
