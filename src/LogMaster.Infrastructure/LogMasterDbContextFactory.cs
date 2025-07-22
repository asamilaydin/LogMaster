using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LogMaster.Infrastructure
{
    public class LogMasterDbContextFactory : IDesignTimeDbContextFactory<LogMasterDbContext>
    {
        public LogMasterDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LogMasterDbContext>();
            // Migration için connection string sabit veriliyor. Gerekirse environment değişkeniyle de alınabilir.
            optionsBuilder.UseNpgsql("Host=localhost;Port=543;Database=logmasterdb;Username=logmasteruser;Password=logmasterpass");
            return new LogMasterDbContext(optionsBuilder.Options);
        }
    }
} 