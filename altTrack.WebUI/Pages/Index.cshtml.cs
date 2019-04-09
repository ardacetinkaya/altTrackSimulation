namespace AltTrack.Web.UI
{
    using AltTrack.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _config;

        [BindProperty]
        public string CustomerName { get; set; }

        [BindProperty]
        public string VehicleId { get; set; }

        [BindProperty]
        public string Status { get; set; }

        [BindProperty]
        public string LastRefresh { get; set; }

        public IEnumerable<Customer> Customers { get; set; }

        public StatusInfo Ping { get; set; }

        public IndexModel(IConfiguration config)
        {

            _config = config;
        }

        public async Task OnGet()
        {
            try
            {
                HttpClient client = new HttpClient()
                {
                    BaseAddress = new Uri(_config["AltTrackBusinessService"])
                };

                var result = await client.GetAsync("/api/customers/all");

                if (result.IsSuccessStatusCode)
                {
                    string resultContent = await result.Content.ReadAsStringAsync();
                    Customers = JsonConvert.DeserializeObject<List<Customer>>(resultContent);
                }
            }
            catch (HttpRequestException)
            {
                //Log or some other business requirments can be done
                //For this demo nothing is needed for an extra
            }

        }

        public async Task<PartialViewResult> OnGetStatusPartial(string vehicleId)
        {
            try
            {
                HttpClient client = new HttpClient()
                {
                    BaseAddress = new Uri(_config["AltTrackPingService"])
                };

                var response = await client.PostAsync("/api/connections/ping/vehicle",
                                        new StringContent(JsonConvert.SerializeObject(new
                                        {
                                            vehicleId
                                        }), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    dynamic responseObject = JsonConvert.DeserializeObject(responseContent);

                    DateTimeOffset time = responseObject.traceTime;
                    string message = responseObject.traceMessage;

                    Ping = new StatusInfo()
                    {
                        Status = message,
                        LastCheck = time
                    };

                    await UpdateVehicleStatus(vehicleId, message, time);

                }
            }
            catch (HttpRequestException)//No connection for a ping server
            {
                //Log or some other business requirments can be done
                //For this demo nothing is needed for an extra
            }

            return Partial("_VehicleStatus", this);
        }

        public async Task OnPostRefreshAsync()
        {
            try
            {
                HttpClient client = new HttpClient()
                {
                    BaseAddress = new Uri(_config["AltTrackBusinessService"])
                };
                var result = await client.GetAsync("/api/customers/all");

                if (result.IsSuccessStatusCode)
                {
                    string resultContent = await result.Content.ReadAsStringAsync();
                    Customers = JsonConvert.DeserializeObject<List<Customer>>(resultContent);
                    LastRefresh = DateTimeOffset.Now.ToString("dd/MM/yyyy hh:mm");
                }
            }
            catch (HttpRequestException)
            {
                //Log or some other business requirments can be done
                //For this demo nothing is needed for an extra
            }

        }

        public async Task OnPostSearchAsync()
        {
            try
            {
                HttpClient client = new HttpClient()
                {
                    BaseAddress = new System.Uri(_config["AltTrackBusinessService"])
                };
                var result = await client.PostAsync("/api/search",
                                      new StringContent(JsonConvert.SerializeObject(new
                                      {
                                          vehicleId = VehicleId,
                                          customerName = CustomerName,
                                          status = Status
                                      }), Encoding.UTF8, "application/json"));

                if (result.IsSuccessStatusCode)
                {
                    string resultContent = await result.Content.ReadAsStringAsync();
                    Customers = JsonConvert.DeserializeObject<List<Customer>>(resultContent);
                }
            }
            catch (HttpRequestException)
            {
                //Log or some other business requirments can be done
                //For this demo nothing is needed for an extra
            }

        }

        private async Task UpdateVehicleStatus(string vehicleId, string message, DateTimeOffset time)
        {
            try
            {
                HttpClient client = new HttpClient()
                {
                    BaseAddress = new System.Uri(_config["AltTrackBusinessService"])
                };

                var result = await client.PostAsync("/api/vehicle/status",
                    new StringContent(JsonConvert.SerializeObject(new
                    {
                        vehicleId = vehicleId,
                        message = message.Trim(),
                        time = time
                    }), Encoding.UTF8, "application/json"));

            }
            catch (HttpRequestException)
            {
                //Log or some other business requirments can be done
                //For this demo nothing is needed for an extra
            }
        }
    }
}
