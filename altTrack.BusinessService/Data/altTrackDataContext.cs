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
            //modelBuilder.Entity<Customer>().HasKey(c => new { c.Id });
            //modelBuilder.Entity<Customer>().Property(c => c.Id).ValueGeneratedOnAdd();

            //modelBuilder.Entity<Vehicle>().HasKey(v => new { v.VehicleId });

            //modelBuilder.Entity<Address>().HasKey(a => new { a.Id });
            //modelBuilder.Entity<Address>().Property(a => a.Id).ValueGeneratedOnAdd();

            //modelBuilder.Entity<Customer>().HasData(
            //    new
            //    {
            //        Id = (long)11,
            //        FirstName = "Kalles",
            //        LastName = "Grustransporter",
            //        AddressId = (long)1
            //    });

            //modelBuilder.Entity<Customer>()
            //            .OwnsOne(d => d.Address)
            //            .HasData(new
            //            {
            //                CustomerId = (long)11,
            //                Id = (long)1,
            //                Street = "Cementvägen 8",
            //                ApartmentNo = 111,
            //                PostalCode = 11,
            //                City = "Södertälje"
            //            });

            //modelBuilder.Entity<Customer>()
            //            .OwnsMany(d => d.Vehicles)
            //            .HasData(
            //                new Vehicle { CustomerId = (long)11, VehicleId = "YS2R4X20005399401", RegistrationNumber = "ABC123", Status = "Unknown" },
            //                new Vehicle { CustomerId = (long)11, VehicleId = "VLUR4X20009093588", RegistrationNumber = "DEF456", Status = "Unknown" },
            //                new Vehicle { CustomerId = (long)11, VehicleId = "VLUR4X20009048066", RegistrationNumber = "GHI789", Status = "Unknown" }
            //            );

            //modelBuilder.Entity<Customer>().HasData(
            //    new
            //    {
            //        Id = (long)22,
            //        FirstName = "Johans",
            //        LastName = "Bulk",
            //        AddressId = (long)2
            //    });

            //modelBuilder.Entity<Customer>()
            //            .OwnsOne(d => d.Address)
            //            .HasData(new
            //            {
            //                CustomerId = (long)22,
            //                Id = (long)2,
            //                Street = "Balkvägen 12",
            //                ApartmentNo = 222,
            //                PostalCode = 22,
            //                City = "Stockholm"
            //            });

            //modelBuilder.Entity<Customer>()
            //            .OwnsMany(d => d.Vehicles)
            //            .HasData(
            //                new Vehicle { CustomerId = 22, VehicleId = "YS2R4X20005388011", RegistrationNumber = "JKL012", Status = "Unknown" },
            //                new Vehicle { CustomerId = 22, VehicleId = "YS2R4X20005387949", RegistrationNumber = "MNO345", Status = "Unknown" }
            //            );

            //modelBuilder.Entity<Customer>().HasData(
            //    new
            //    {
            //        Id = (long)33,
            //        FirstName = "Harals",
            //        LastName = "Vardetransporter",
            //        AddressId = (long)3
            //    });

            //modelBuilder.Entity<Customer>()
            //            .OwnsOne(d => d.Address)
            //            .HasData(new
            //            {
            //                CustomerId = (long)33,
            //                Id = (long)3,
            //                Street = "Budgetvägen 1",
            //                ApartmentNo = 333,
            //                PostalCode = 33,
            //                City = "Uppsala"
            //            });


            //modelBuilder.Entity<Customer>()
            //            .OwnsMany(d => d.Vehicles)
            //            .HasData(
            //               // new Vehicle { CustomerId = 33, VehicleId = "VLUR4X20009048066", RegistrationNumber = "PQR678", Status = "Unknown" },
            //                new Vehicle { CustomerId = 33, VehicleId = "YS2R4X20005387055", RegistrationNumber = "STU901", Status = "Unknown" }
            //                );

        }
    }
}
