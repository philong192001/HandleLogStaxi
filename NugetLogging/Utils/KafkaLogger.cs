using Microsoft.Extensions.Logging;

namespace NugetLogging.Utils;

public class KafkaLogger : ILogger
{
    private readonly string _categoryName;
    private readonly KafkaProducer _kafkaProducer;

    public KafkaLogger(string categoryName, KafkaProducer kafkaProducer)
    {
        _categoryName = categoryName;
        _kafkaProducer = kafkaProducer;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null; // You can implement this if needed for scoping
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        // Decide whether to log based on log level
        return true; // Always log
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        // Định dạng và gửi log tới Kafka
        string logMessage = formatter(state, exception);
        string formattedLog = $"[{DateTime.UtcNow}] [{_categoryName}] [{logLevel}] {logMessage}";

        // Gửi log tới Kafka
        _kafkaProducer.SendLogMessage(formattedLog);
    }
}
