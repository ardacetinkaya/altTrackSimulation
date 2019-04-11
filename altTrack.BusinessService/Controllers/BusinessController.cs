namespace AltTrack.BusinessService.Controllers
{
    using AltTrack.BusinessService.Business;
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
        private readonly AltTrackBusiness _business = null;

        public BusinessController(IDataRepository dataRepository, IConfiguration config)
        {
            _dataRepository = dataRepository;
            _config = config;

            _business = new AltTrackBusiness(_dataRepository, config["TimeLimit"]);

        }

        [HttpGet]
        [Route("api/customers/all")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
        {
            IEnumerable<Customer> response = null;
            try
            {
                response = await _business.GetAllTrackingInfo(async vehicleId =>
                 {
                     return await GetLastStatus(vehicleId);
                 });
            }
            catch (Exception ex)
            {

                return BadRequest($"Unexpected error: {ex.Message}");
            }


            return Ok(response);
        }

        [HttpGet]
        [Route("api/vehicle/{vehicleId}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(string vehicleId)
        {
            Vehicle response = null;
            try
            {
                response = await _business.GetVehicle(vehicleId);

                if (response == null)
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {

                return BadRequest($"Unexpected error: {ex.Message}");
            }

            return Ok(response);
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

            if (!_business.IsVehicleExists(body.VehicleId))
            {
                return BadRequest("Vehicle does not exists in system.");
            }

            var result =_business.UpdateVehicleStatus(body.VehicleId.Trim(), body.Message, body.Time);

            if (!result)
            {
                return BadRequest("Can not update vehicle status.");
            }

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

            IEnumerable<Customer> response = null;

            try
            {
                response = await _business.Search(body.CustomerName, body.VehicleId, body.Status);

                if (response == null || response.Count() <= 0)
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {

                return BadRequest($"Unexpected error: {ex.Message}");
            }


            return Ok(response);
        }

        private async Task<PingStatus> GetLastStatus(string vehicleId)
        {
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
                        return new PingStatus
                        {
                            Status = statusResult.traceMessage,
                            LastCheckDate = statusResult.traceTime

                        };

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error occured: {ex.Message}");
            }
            return new PingStatus
            {
                Status = "Unknown",
                LastCheckDate = DateTimeOffset.Now
            };

        }
    }

}
