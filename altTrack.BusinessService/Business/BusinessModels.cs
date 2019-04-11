namespace AltTrack.BusinessService.Business
{
    using System;

    public class PingStatus
    {
        public string Status { get; set; }
        public DateTimeOffset LastCheckDate { get; set; }
    }
}
