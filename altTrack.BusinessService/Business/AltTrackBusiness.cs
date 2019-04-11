namespace AltTrack.BusinessService.Business
{
    using AltTrack.BusinessService.Data;
    using AltTrack.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AltTrackBusiness
    {
        private readonly IDataRepository _dataRepository;
        private readonly int _timeLimit;

        public AltTrackBusiness(IDataRepository dataRepository, string timeLimitConfig)
        {
            _dataRepository = dataRepository;

            //Default is 10
            _timeLimit = !Int32.TryParse(timeLimitConfig, out _timeLimit) ? 10 : _timeLimit;

        }

        public async Task<IEnumerable<Customer>> GetAllTrackingInfo(Func<string, Task<PingStatus>> getLastCheck)
        {
            if (getLastCheck == null)
            {
                throw new ArgumentNullException(nameof(getLastCheck));
            }

            var vehicles = await GetVehicles();

            if (vehicles != null)
            {
                //Check vehicles' latest status.
                vehicles.ToList().ForEach(v =>
                {
                    var lastCheck = getLastCheck(v.Id).Result;

                    var status = CheckStatus(lastCheck.LastCheckDate);
                    status = string.IsNullOrEmpty(status) ? lastCheck.Status : status;

                    _dataRepository.UpdateVehicleStatus(v.Id, status, lastCheck.LastCheckDate);
                });


                var trackingData = await _dataRepository.GetCustomers();

                return trackingData;
            }

            return null;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var result = await _dataRepository.GetCustomers();

            return result;
        }

        public async Task<IEnumerable<Vehicle>> GetVehicles()
        {
            var result = await _dataRepository.GetVehicles();

            return result;
        }

        public string CheckStatus(DateTimeOffset time)
        {
            var status = string.Empty;
            DateTimeOffset startTime = time;
            TimeSpan span = DateTimeOffset.Now.Subtract(startTime);

            //No status check for {config_value} mins. means that the vehicle is disconnected
            if (span.Minutes >= _timeLimit)
            {
                status = "Disconnected";
            }

            return status;
        }

        public Task<Vehicle> GetVehicle(string vehicleId)
        {
            if (string.IsNullOrEmpty(vehicleId))
            {
                return null;
            }

            return _dataRepository.GetVehicle(vehicleId);

        }

        public bool UpdateVehicleStatus(string vehicleId, string message, DateTimeOffset time)
        {
            //Vehicle id can not be blank
            if (string.IsNullOrEmpty(vehicleId))
            {
                return false;
            }

            if (string.IsNullOrEmpty(message))
            {
                return false;
            }

            _dataRepository.UpdateVehicleStatus(vehicleId, message, time);

            return true;
        }

        public bool IsVehicleExists(string vehicleId)
        {
            if (string.IsNullOrEmpty(vehicleId))
            {
                return false;
            }

            return _dataRepository.IsVehicleExists(vehicleId);
        }

        public Task<IEnumerable<Customer>> Search(string customerName, string vehicleId, string status)
        {
            return _dataRepository.Search(customerName, vehicleId, status);
        }
    }
}
