using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NugetLogging.Providers;
using NugetLogging.Utils;

namespace NugetLogging.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        // Đăng ký KafkaProducer và KafkaLoggerProvider
        var kafkaProducer = new KafkaProducer("kafka1:9092,kafka2:9093,kafka3:9094");
        services.AddSingleton(kafkaProducer);
        services.AddSingleton<ILoggerProvider>(provider => new KafkaLoggerProvider(kafkaProducer));

        return services;
    }
}
