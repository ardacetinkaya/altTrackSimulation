namespace AltTrack.BusinessService.Data
{
    using AltTrack.Data;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDataRepository
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<IEnumerable<Vehicle>> GetVehicles();
        void UpdateVehicleStatus(string vehicleId, string status, DateTimeOffset time);
        Task<IEnumerable<Customer>> Search(string customerName, string vehicleId,string status);
        Task<Vehicle> GetVehicle(string vehicleId);
        bool IsVehicleExists(string vehicleId);
    }

}