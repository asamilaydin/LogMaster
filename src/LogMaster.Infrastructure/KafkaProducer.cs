using System.Threading.Tasks;
using Confluent.Kafka;
using Newtonsoft.Json;
using LogMaster.Domain;
using Microsoft.Extensions.Configuration;

namespace LogMaster.Infrastructure
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly IProducer<Null, string> _producer;
        private readonly string _topic;

        public KafkaProducer(IConfiguration config)
        {
            var bootstrapServers = config["Kafka:BootstrapServers"];
            _topic = config["Kafka:Topic"];

            var configProducer = new ProducerConfig {
                BootstrapServers = bootstrapServers,
                SocketTimeoutMs = 3000,
                MessageTimeoutMs = 3000,
                ReconnectBackoffMs = 500,
                ReconnectBackoffMaxMs = 1000,
                // Retries = 0 // (isteğe bağlı, Kafka'ya hiç retry yapmasın istersen açabilirsin)
            };
            _producer = new ProducerBuilder<Null, string>(configProducer).Build();
        }

        public async Task ProduceAsync(LogEntry log)
        {
            var json = JsonConvert.SerializeObject(log);
            await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = json });
        }
    }
}
