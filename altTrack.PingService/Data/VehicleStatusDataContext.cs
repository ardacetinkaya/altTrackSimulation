using Microsoft.EntityFrameworkCore;

namespace AltTrack.PingService.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        { }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<VehicleStatus> VehicleStatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
            }
        }

    }
}
