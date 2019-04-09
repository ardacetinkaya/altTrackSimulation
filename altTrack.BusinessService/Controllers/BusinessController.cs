namespace altTrack.BusinessService.Controllers
{
    using AltTrack.BusinessService.Data;
    using AltTrack.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly IDataRepository _dataRepository = null;
        private readonly IConfiguration _config = null;

        //Difference limit between current time and last connection check
        private int _minuteDifference = 10;

        public BusinessController(IDataRepository dataRepository, IConfiguration config)
        {
            _dataRepository = dataRepository;
            _config = config;
        }

        [HttpGet]
        [Route("api/customers/all")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
        {
            var vehicles = await _dataRepository.GetVehicles();

            //Check vehicles' latest status.
            vehicles.ToList().ForEach(v =>
            {
                var status = CheckStatus(v.VehicleId);
                _dataRepository.UpdateVehicleStatus(v.VehicleId, status.Result, DateTimeOffset.Now);
            });

            var customers = await _dataRepository.GetCustomers();

            if (customers == null)
            {
                return NoContent();
            }

            return Ok(customers);
        }

        [HttpGet]
        [Route("api/vehicle/{vehicleId}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(string vehicleId)
        {
            var vehicle = await _dataRepository.GetVehicle(vehicleId);

            if (vehicle == null)
            {
                return NoContent();
            }
            return Ok(vehicle);
        }

        [HttpPost]
        [Route("api/vehicle/status")]
        public ActionResult UpdateVehicleStatus([FromBody]StatusDTO body)
        {
            if (body == null)
            {
                return BadRequest("Invalid body.");
            }

            if (string.IsNullOrEmpty(body.VehicleId))
            {
                return BadRequest("Invalid vehicle id.");
            }

            if (!_dataRepository.IsVehicleExists(body.VehicleId))
            {
                return BadRequest("Vehicle does not exists in system.");
            }

            _dataRepository.UpdateVehicleStatus(body.VehicleId.Trim(), body.Message, body.Time);

            return Ok();
        }

        [HttpPost]
        [Route("api/search")]
        public async Task<ActionResult<IEnumerable<Customer>>> Search([FromBody]SearchDTO body)
        {
            if (body == null)
            {
                return BadRequest("Invalid body.");
            }

            var result = await _dataRepository.Search(body.CustomerName, body.VehicleId, body.Status);

            if (result == null || result.Count() <= 0)
            {
                return NoContent();
            }

            return Ok(result);
        }

        private async Task<string> CheckStatus(string vehicleId)
        {
            var status = "Unknown";
            var time = DateTimeOffset.Now;
            try
            {
                HttpClient client = new HttpClient()
                {
                    BaseAddress = new Uri(_config["AltTrackPingService"])
                };
                var result = await client.GetAsync($"api/connections/status/latest/{vehicleId}");


                if (result.IsSuccessStatusCode)
                {
                    string resultContent = await result.Content.ReadAsStringAsync();
                    dynamic statusResult = JsonConvert.DeserializeObject(resultContent);
                    if (statusResult != null)
                    {
                        status = statusResult.traceMessage;
                        time = statusResult.traceTime;

                        DateTimeOffset startTime = time;
                        TimeSpan span = DateTimeOffset.Now.Subtract(startTime);

                        //No status check for 10 mins. means that the vehicle is disconnected
                        if (span.Minutes >= _minuteDifference)
                        {
                            status = "Disconnected";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                //Log exception or do some other related business stuff
                status = $"Error: {ex.Message} URI:{_config["AltTrackPingService"]}";
            }
            return status;

        }
    }

}
