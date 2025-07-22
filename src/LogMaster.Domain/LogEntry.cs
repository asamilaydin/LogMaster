namespace LogMaster.Domain
{
    public class LogEntry
    {
        public string? Level { get; set; }
        public string? Message { get; set; }
        public string? Source { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
