using Microsoft.EntityFrameworkCore;

namespace LogMaster.Infrastructure
{
    public class LogMasterDbContext : DbContext
    {
        public LogMasterDbContext(DbContextOptions<LogMasterDbContext> options) : base(options) { }

        public DbSet<FailedLogEntry> FailedLogEntries { get; set; }
    }
} 