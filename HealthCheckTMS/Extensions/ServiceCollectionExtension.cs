using HealthCheckTMS.HealthCheckCustoms;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthCheckTMS.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterService(this IServiceCollection services, IConfiguration configuration)
    {
        AddHC(services, configuration);

        return services;
    }

    public static IServiceCollection AddHC(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
           //.AddCheck<RemoteHealthCheck>("Remote endpoints Health Check", failureStatus: HealthStatus.Unhealthy)
           //.AddSqlServer("Server=10.0.10.22,11433\\basql;Database=VNPayV2;User Id=tunglx;Password=tunglx123!@#;Integrated Security=False;", healthQuery: "select 1", name: "SQL Server 03", failureStatus: HealthStatus.Unhealthy, tags: new[] { "Feedback", "Database" })
           .AddSqlServer("Server=10.0.5.22,11433\\basql;Database=BakG7;User Id=g7;Password=g7123!@#;Integrated Security=False;Encrypt=True;TrustServerCertificate=True;", healthQuery: "select 1",name: "SQL Server g7bak", failureStatus: HealthStatus.Unhealthy, tags: new[] { "Feedback", "Database" })
           //.AddRedis("10.0.10.70:6379", "Redis", failureStatus: HealthStatus.Unhealthy, tags: new[] { "Feedback", "Redis" })
           //.AddMongoDb("mongodb://tunglx:tunglx@192.168.1.51:27017", name: "mongodb", failureStatus: HealthStatus.Unhealthy, tags: new[] { "mongodb" })
           ;

        services.AddHealthChecksUI(opt =>
        {
            //opt.SetEvaluationTimeInSeconds(10); //time in seconds between check    
            //opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks    
            //opt.SetApiMaxActiveRequests(1); //api requests concurrency
            //opt.AddHealthCheckEndpoint("floor api", "http://g7bak.staxi.vn:12621/api/FloorLandmark/SuperviseLobbyStaff/209"); //map health check api    
            opt.AddHealthCheckEndpoint("report api", "http://reportg7bak.staxi.vn/api/ReportTrip"); //map health check api
        })
        .AddInMemoryStorage();
        return services;
    }
}