using ManageVoyage.BackgroundTasks;
using ManageVoyage.Data;
using ManageVoyage.Repositories;
using ManageVoyage.Settings;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ManageVoyage.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterService(this IServiceCollection services, IConfiguration configuration, WebApplicationBuilder builder)
    {
        var appSetting = AppSetting.MapValue(configuration);
        services.AddSingleton(appSetting);
        services.AddHttpContextAccessor();
        services.AddScoped<ICaroBookRepository, CaroBookRepository>();
        services.AddHttpClient();
        services.AddDbContext<CaroBookingContext>(x => x.UseSqlServer(appSetting.ConnectionString, options =>
        {
            options.EnableRetryOnFailure();
        }));
        services.AddHostedService<GetVoyageBackgroundTasks>();
        services.AddLogger(configuration,builder);
        return services;
    }

    public static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration configuration, WebApplicationBuilder builder)
    {
        var logger = new LoggerConfiguration()
           .ReadFrom.Configuration(configuration)
           .Enrich.FromLogContext()
           .CreateLogger();
        builder.Logging.AddSerilog(logger);

        return services;
    }
}
