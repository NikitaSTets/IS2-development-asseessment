using DataExporter.Interfaces;
using DataExporter.Services;
using Data;
using DataExporter.ManagedResponse;
using DataExporter.Filters;

namespace DataExporter;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddDataAccessLayer();
        builder.Services.AddScoped<IPolicyService, PolicyService>();
        builder.Services.AddTransient<ManagedResultFactory>();
        builder.Services
                .AddControllers(options =>
                {
                    options.Filters.Add<ManagedExceptionFilter>();
                });

        var app = builder.Build();

        //better to use migration because it can only be run once
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ExporterDbContext>();
            dbContext.Database.EnsureCreated();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
