namespace PingService.Controllers
{
    using AltTrack.PingService.Model;
    using AltTrack.PingService.Data;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    public class ConnectionController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;
        public ConnectionController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpPost]
        [Route("api/connections/ping/vehicle")]
        public async Task<IActionResult> PingVehicle([FromBody] PingMessage message)
        {

            if (message == null || string.IsNullOrEmpty(message.VehicleId))
            {
                return BadRequest("Invalid vehicle id.");
            }

            VehicleStatus response = null;

            try
            {
                //This part is all simulation. Assume that this is kind of a real ping for vehicles' network
                //Make a ping request and process the response.
                //This simulation randomly acts as "Connected","Disconnected" and "Unknown" response.
                //Connected    : Vehicle is running
                //Disconnected : Vehicle is not running
                //Unknown      : Can not reach vehicle due to some network/connection problems
                List<string> responses = new List<string>() { "Connected", "Disconnected", "Unknown" };
                Random random = new Random();
                int index = random.Next(responses.Count);
                var dummyResponse = responses[index];

                //Save simulated response to service's data storage
                //In fact in this scenario, this might be ackword.
                //But the main idea is store every service's data to its own data storage
                response = await _dataRepository.AddStatus(message.VehicleId.Trim(), dummyResponse);
            }
            catch (Exception ex)
            {
                return BadRequest($"Unexpected error: {ex.Message}");
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("api/connections/ping/base")]
        public async Task<IActionResult> PingBase([FromBody] PingMessage message)
        {
            if (message == null || string.IsNullOrEmpty(message.VehicleId))
            {
                return BadRequest("Invalid vehicle id.");
            }
            try
            {
                await _dataRepository.AddStatus(message.VehicleId.Trim(), "Connected");
            }
            catch (Exception ex)
            {
                return BadRequest($"Unexpected error: {ex.Message}");
            }


            return Ok();
        }

        [HttpGet]
        [Route("api/connections/status/latest/{vehicleId}")]
        public async Task<IActionResult> GetLatestPingStatus(string vehicleId)
        {
            if (string.IsNullOrEmpty(vehicleId))
            {
                return BadRequest("Invalid vehicle id.");
            }

            VehicleStatus response = null;
            try
            {
                response = await _dataRepository.GetLastStatus(vehicleId);

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
    }
}
