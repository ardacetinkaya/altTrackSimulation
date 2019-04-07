namespace AltTrack.BusinessService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AltTrack.Data;
    using Microsoft.EntityFrameworkCore;

    public class AltTrackDataRepository : IDataRepository
    {
        private AltTrackDataContext _dataContext;

        public AltTrackDataRepository(AltTrackDataContext context)
        {
            _dataContext = context;

            //Fill seed data
            _dataContext.Database.EnsureCreated();
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var customers = await _dataContext.Customers.ToListAsync();

            return customers;
        }
        public async Task<Vehicle> GetVehicle(string vehicleId)
        {
            var vehicle = await _dataContext.Vehicles.Where(v => v.VehicleId == vehicleId).FirstOrDefaultAsync();

            return vehicle;
        }

        public async Task<IEnumerable<Vehicle>> GetVehicles()
        {
            var vehicles = await _dataContext.Vehicles.ToListAsync();

            return vehicles;
        }

        public bool IsVehicleExists(string vehicleId)
        {
            var vehicle = _dataContext.Vehicles.Where(v => v.VehicleId == vehicleId).FirstOrDefault();

            return !(vehicle == null);
        }

        public async Task<IEnumerable<Customer>> Search(string customerName, string vehicleId, string status)
        {
            var customers = await _dataContext.Customers
                .Where(c => null == customerName || $"{c.FirstName} {c.LastName}".ToLower().Contains(customerName.ToLower()))
                .Where(c => null == vehicleId || c.Vehicles.Where(v => v.VehicleId.Contains(vehicleId.ToUpper())).Any())
                .ToListAsync();

            customers.ForEach(c => c.Vehicles = c.Vehicles
                                                .Where(v => null == vehicleId || v.VehicleId.Contains(vehicleId.ToUpper()))
                                                .ToList());

            customers.ForEach(c => c.Vehicles = c.Vehicles
                                    .Where(v => null == status || v.Status.Contains(status))
                                    .ToList());
            return customers;
        }

        public void UpdateVehicleStatus(string vehicleId, string status, DateTimeOffset time)
        {
            var vehicle = _dataContext.Vehicles.Where(v => v.VehicleId == vehicleId).FirstOrDefault();
            if (vehicle != null)
            {
                vehicle.Status = status;
                vehicle.LastCheck = time;
                _dataContext.SaveChanges();
            }
        }
    }
}