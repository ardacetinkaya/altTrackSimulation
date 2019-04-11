using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltTrack.BusinessService
{
    public class StatusDTO
    {
        public string VehicleId { get; set; }
        public string Message { get; set; }
        public DateTimeOffset Time { get; set; }
    }

    public class SearchDTO
    {
        public string CustomerName { get; set; }
        public string VehicleId { get; set; }
        public string Status { get; set; }
    }
}
