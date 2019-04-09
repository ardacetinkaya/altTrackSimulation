namespace AltTrack.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Customer
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public Address Address { get; set; }
		public List<CustomerVehicle> Vehicles { get; set; }
	}

	public class Address
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }
        public string Street{ get; set; }
		public int ApartmentNo { get; set; }
		public int PostalCode { get; set; }
		public string City { get; set; }
        public long CustomerId { get; set; }

    }

    public class CustomerVehicle
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long CustomerId { get; set; }
        public string RegistrationNumber { get; set; }

        public string VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
	public class Vehicle
	{
        public string Id { get; set; }
        public string Brand { get; set; }
        public string LastStatus { get; set; }
        public DateTimeOffset? LastPing { get; set; }
    }
}
