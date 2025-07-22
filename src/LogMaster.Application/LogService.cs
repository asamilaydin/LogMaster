using System;
using System.Threading.Tasks;
using LogMaster.Domain;
using LogMaster.Infrastructure;

namespace LogMaster.Application
{
    public class LogService
    {
        private readonly IKafkaProducer _kafkaProducer;
        private readonly LogMasterDbContext _dbContext;

        public LogService(IKafkaProducer kafkaProducer, LogMasterDbContext dbContext)
        {
            _kafkaProducer = kafkaProducer;
            _dbContext = dbContext;
        }

        public async Task SendLogAsync(LogEntry log)
        {
            if (string.IsNullOrWhiteSpace(log.Level) || string.IsNullOrWhiteSpace(log.Message))
                throw new ArgumentException("Level and Message are required.");

            log.Timestamp = DateTime.UtcNow;

            try
            {
                await _kafkaProducer.ProduceAsync(log);
            }
            catch (Exception ex)
            {
                // Kafka'ya gönderilemeyen logu veritabanına kaydet
                var failedLog = new FailedLogEntry
                {
                    Level = log.Level,
                    Message = log.Message,
                    Source = log.Source,
                    Timestamp = log.Timestamp,
                    Exception = ex.ToString()
                };
                _dbContext.FailedLogEntries.Add(failedLog);
                await _dbContext.SaveChangesAsync();
                throw; // Exception'ı tekrar fırlat, API'de hata olarak dönsün
            }
        }
    }
}
