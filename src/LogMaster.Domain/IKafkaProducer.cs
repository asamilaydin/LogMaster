using System.Threading.Tasks;

namespace LogMaster.Domain
{
    public interface IKafkaProducer
    {
        Task ProduceAsync(LogEntry log);
    }
}
