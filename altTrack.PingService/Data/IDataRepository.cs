using System.Collections.Generic;
using System.Threading.Tasks;

namespace AltTrack.PingService.Data
{
    public interface IDataRepository
    {
        IEnumerable<VehicleStatus> GetAllStatuses(string vehicleId);
        Task<VehicleStatus> GetLastStatus(string vehicleId);
        Task<VehicleStatus> AddStatus(string vehicleId,string message);
    }

}