namespace altTrack.Vehicle
{
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// This is a simulation for vehicle
    /// This an Azure Function with Time trigger property to simulate vehicles' ping feature
    /// Vehicles ping the base(headquarter) to indicate their status
    /// </summary>
    public static class Vehicle
    {
        [FunctionName("Vehicle")]
        public static async Task Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {

            List<string> _vehicles = new List<string>()
            {
                "YS2R4X20005399401",
                "VLUR4X20009093588",
                "VLUR4X20009048066",
                "YS2R4X20005388011",
                "YS2R4X20005387949",
                "VLUR4X20009048096",
                "YS2R4X20005387055"
            };

            var random = new Random();
            var index = random.Next(_vehicles.Count);

            var vehicleSimulation = _vehicles[index];

            try
            {
                HttpClient client = new HttpClient()
                {
                    BaseAddress = new Uri("https://alttrack-pingservice.azurewebsites.net")
                };
                var result = await client.PostAsync("/api/connections/ping/base",
                    new StringContent(JsonConvert.SerializeObject(new
                    {
                        vehicleId = vehicleSimulation
                    }), Encoding.UTF8, "application/json"));

                log.LogInformation($"Vehicle({vehicleSimulation}) simulation executed at: {DateTime.Now}");
                log.LogInformation($"Result: Success");
            }
            catch (Exception ex)
            {
                log.LogInformation($"Vehicle({vehicleSimulation}) simulation had an error at {DateTime.Now}");
                log.LogInformation($"Error: {ex.Message}");
            }
        }
    }
}
