namespace AltTrack.PingService.Data
{
    using Microsoft.EntityFrameworkCore;

    public class DataContext : DbContext
    {
        public DataContext()
        { }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<VehicleStatus> VehicleStatus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

    }
}
