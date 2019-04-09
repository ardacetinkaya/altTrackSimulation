namespace AltTrack.PingService.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class VehicleStatus
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }
		public string VehicleId { get; set; }
		public DateTimeOffset TraceTime { get; set; }
		public string TraceMessage { get; set; }
	}
}
