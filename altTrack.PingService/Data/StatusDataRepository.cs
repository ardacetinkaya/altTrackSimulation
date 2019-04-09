namespace AltTrack.PingService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class StatusDataRepository : IDataRepository
    {
        private readonly DataContext _dataContext;

        public StatusDataRepository(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<VehicleStatus> AddStatus(string vehicleId, string message)
        {
            var status = new VehicleStatus()
            {
                TraceTime = DateTimeOffset.Now,
                TraceMessage = message.Trim(),
                VehicleId = vehicleId
            };

            _dataContext.VehicleStatus.Add(status);
            await _dataContext.SaveChangesAsync();

            return status;
        }

        public IEnumerable<VehicleStatus> GetAllStatuses(string vehicleId)
        {
            return _dataContext.VehicleStatus.ToList();
        }

        public Task<VehicleStatus> GetLastStatus(string vehicleId)
        {
            var status = _dataContext.VehicleStatus.Where(s => s.VehicleId == vehicleId).OrderByDescending(s => s.TraceTime).FirstOrDefaultAsync();

            return status;
        }
    }
}