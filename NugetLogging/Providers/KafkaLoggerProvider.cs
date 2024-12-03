using Microsoft.Extensions.Logging;
using NugetLogging.Utils;

namespace NugetLogging.Providers;

public class KafkaLoggerProvider : ILoggerProvider
{
    private readonly KafkaProducer _kafkaProducer;

    public KafkaLoggerProvider(KafkaProducer kafkaProducer)
    {
        _kafkaProducer = kafkaProducer ?? throw new ArgumentNullException(nameof(kafkaProducer));
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new KafkaLogger(categoryName, _kafkaProducer);
    }

    public void Dispose()
    {
        // Đóng kết nối Kafka khi không cần thiết nữa
        _kafkaProducer.Dispose();
    }
}
