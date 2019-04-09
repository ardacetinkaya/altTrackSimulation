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
            var customers = await _dataContext.Customer.Include(c => c.Address).Include(v => v.Vehicles).ToListAsync();

            return customers;
        }
        public async Task<Vehicle> GetVehicle(string vehicleId)
        {
            var vehicle = await _dataContext.Vehicle.Where(v => v.Id == vehicleId).FirstOrDefaultAsync();

            return vehicle;
        }

        public async Task<IEnumerable<Vehicle>> GetVehicles()
        {
            var vehicles = await _dataContext.Vehicle.ToListAsync();

            return vehicles;
        }

        public bool IsVehicleExists(string vehicleId)
        {
            var vehicle = _dataContext.Vehicle.Where(v => v.Id == vehicleId).FirstOrDefault();

            return !(vehicle == null);
        }

        public async Task<IEnumerable<Customer>> Search(string customerName, string vehicleId, string status)
        {
            var result = await _dataContext.Customer
                .Include(c => c.Address)
                .Include(v => v.Vehicles)
                    .ThenInclude(vehicle => vehicle.Vehicle)
                .Where(c => null == customerName || $"{c.FirstName} {c.LastName}".ToLower().Contains(customerName.ToLower()))
                .Where(c => null == vehicleId || c.Vehicles.Where(v => v.VehicleId.Contains(vehicleId.ToUpper())).Any())
                .ToListAsync();

            result.ForEach(c => c.Vehicles=c.Vehicles.Where(v => null == vehicleId || v.VehicleId.Contains(vehicleId.ToUpper())).ToList());

            result.ForEach(c => c.Vehicles = c.Vehicles
                                    .Where(v => null == status || v.Vehicle.LastStatus.Contains(status))
                                    .ToList());

            return result;
        }

        public void UpdateVehicleStatus(string vehicleId, string status, DateTimeOffset time)
        {
            var vehicle = _dataContext.Vehicle.Where(v => v.Id == vehicleId).FirstOrDefault();
            if (vehicle != null)
            {
                vehicle.LastStatus = status;
                vehicle.LastPing = time;
                _dataContext.SaveChanges();
            }
        }
    }
}