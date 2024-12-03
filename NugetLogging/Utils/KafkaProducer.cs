using Confluent.Kafka;

namespace NugetLogging.Utils;

public class KafkaProducer : IDisposable
{
    private readonly ProducerConfig _producerConfig;
    private readonly IProducer<Null, string> _producer;

    public KafkaProducer(string bootstrapServers)
    {
        _producerConfig = new ProducerConfig { BootstrapServers = bootstrapServers };
        _producer = new ProducerBuilder<Null, string>(_producerConfig).Build();
    }

    public void SendLogMessage(string logMessage)
    {
        var message = new Message<Null, string> { Value = logMessage };

        // Gửi log message tới topic của Kafka
        _producer.ProduceAsync("logs-topic", message);
    }

    public void Dispose()
    {
        // Đóng kết nối Kafka khi không cần thiết nữa
        _producer.Dispose();
    }
}
