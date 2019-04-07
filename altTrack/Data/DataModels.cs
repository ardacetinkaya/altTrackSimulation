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
		public long AddressId { get; set; }
		public Address Address { get; set; }
		public List<Vehicle> Vehicles { get; set; }
	}

	public class Address
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }
		public string Street{ get; set; }
		public int ApartmentNo { get; set; }
		public int PostalCode { get; set; }
		public string City { get; set; }
	}

	public class Vehicle
	{
		public long CustomerId { get; set; }
		public string VehicleId { get; set; }
		public string RegistrationNumber { get; set; }

        public string Status { get; set; }
        public DateTimeOffset? LastCheck { get; set; }
    }
}
