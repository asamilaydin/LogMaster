using System;

namespace LogMaster.Infrastructure
{
    public class FailedLogEntry
    {
        public int Id { get; set; }
        public string? Level { get; set; }
        public string? Message { get; set; }
        public string? Source { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Exception { get; set; }
    }
} 