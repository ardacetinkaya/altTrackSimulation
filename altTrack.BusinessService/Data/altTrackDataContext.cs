namespace AltTrack.BusinessService.Data
{
    using AltTrack.Data;
    using Microsoft.EntityFrameworkCore;
    public class AltTrackDataContext : DbContext
    {
        public AltTrackDataContext()
        { }

        public AltTrackDataContext(DbContextOptions<AltTrackDataContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<CustomerVehicle> CustomerVehicle { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            if (this.Database.IsInMemory())
            {
                modelBuilder.Entity<Customer>().HasData(
                    new Customer { Id = (long)1, FirstName = "Kalles", LastName = "Grustransporter" },
                    new Customer { Id = (long)2, FirstName = "Johans", LastName = "Bulk" },
                    new Customer { Id = (long)3, FirstName = "Harals", LastName = "Vardetransporter" }
                );

                modelBuilder.Entity<Address>().HasData(
                    new Address { CustomerId = (long)1, Id = (long)1, Street = "Cementvägen 8", ApartmentNo = 111, PostalCode = 11, City = "Södertälje" },
                    new Address { CustomerId = (long)2, Id = (long)2, Street = "Balkvägen 12", ApartmentNo = 222, PostalCode = 22, City = "Stockholm" },
                    new Address { CustomerId = (long)3, Id = (long)3, Street = "Budgetvägen 1", ApartmentNo = 333, PostalCode = 33, City = "Uppsala" }
                );

                modelBuilder.Entity<Vehicle>().HasData(
                    new Vehicle { Id = "YS2R4X20005399401", Brand = "Volvo" },
                    new Vehicle { Id = "VLUR4X20009093588", Brand = "Volvo" },
                    new Vehicle { Id = "VLUR4X20009048066", Brand = "Volvo" },
                    new Vehicle { Id = "YS2R4X20005388011", Brand = "Volvo" },
                    new Vehicle { Id = "YS2R4X20005387949", Brand = "Volvo" },
                    new Vehicle { Id = "YS2R4X20005387055", Brand = "Volvo" }
                   );

                modelBuilder.Entity<CustomerVehicle>().HasData(
                    new CustomerVehicle { Id = 1, CustomerId = 1, RegistrationNumber = "ABC123", VehicleId = "YS2R4X20005399401" },
                    new CustomerVehicle { Id = 2, CustomerId = 1, RegistrationNumber = "DEF456", VehicleId = "VLUR4X20009093588" },
                    new CustomerVehicle { Id = 3, CustomerId = 1, RegistrationNumber = "GHI789", VehicleId = "VLUR4X20009048066" },
                    new CustomerVehicle { Id = 4, CustomerId = 2, RegistrationNumber = "JKL012", VehicleId = "YS2R4X20005388011" },
                    new CustomerVehicle { Id = 5, CustomerId = 2, RegistrationNumber = "MNO345", VehicleId = "YS2R4X20005387949" },
                    new CustomerVehicle { Id = 6, CustomerId = 3, RegistrationNumber = "PQR678", VehicleId = "VLUR4X20009048066" },
                    new CustomerVehicle { Id = 7, CustomerId = 3, RegistrationNumber = "STU901", VehicleId = "YS2R4X20005387055" }
                    );
            }
        }
    }
}
