namespace AltTrack.BusinessService.Test
{
    using AltTrack.BusinessService.Business;
    using AltTrack.BusinessService.Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;


    //This is just some tests for Business Service
    public class BusinessTests
    {
        [Fact]
        public async Task ConnectedVehicleCountTest()
        {
            var options = new DbContextOptionsBuilder<AltTrackDataContext>()
                 .UseInMemoryDatabase(databaseName: "dummy")
                 .Options;

            AltTrackDataRepository dataRepository = new AltTrackDataRepository(new AltTrackDataContext(options));
            AltTrackBusiness business = new AltTrackBusiness(dataRepository, "10");

            //This should be update all 7 vehicles as Connected
            var result = await business.GetAllTrackingInfo(async vehicleId =>
            {
                return await MockStatus("Connected");

            });


            var actualStatusCount = 0;

            //Check the exact connected count
            var check = result.Select(c =>
              {
                  actualStatusCount += c.Vehicles.Where(v => v.Vehicle.LastStatus == "Connected").Select(v => v.Vehicle).ToList().Count;
                  return c;
              }).ToList();

            //In dummy data there is 7 total vehicles, so expected also 7
            //Let's check actual methods result
            Assert.Equal(7, actualStatusCount);
        }

        [Fact]
        public void TimeLimitPassTest()
        {
            var options = new DbContextOptionsBuilder<AltTrackDataContext>()
                .UseInMemoryDatabase(databaseName: "dummy")
                .Options;

            AltTrackDataRepository dataRepository = new AltTrackDataRepository(new AltTrackDataContext(options));
            AltTrackBusiness business = new AltTrackBusiness(dataRepository, "10");

            string actual = business.CheckStatus(DateTimeOffset.Now.Subtract(new TimeSpan(0, 30, 0)));

            //Status should be disconnected because it passes 30 minutes over config's 10 value
            Assert.Equal("Disconnected", actual);
        }

        [Fact]
        public void TimeLimitTest()
        {
            var options = new DbContextOptionsBuilder<AltTrackDataContext>()
                .UseInMemoryDatabase(databaseName: "dummy")
                .Options;

            AltTrackDataRepository dataRepository = new AltTrackDataRepository(new AltTrackDataContext(options));
            AltTrackBusiness business = new AltTrackBusiness(dataRepository, "40");

            string actual = business.CheckStatus(DateTimeOffset.Now.Subtract(new TimeSpan(0, 30, 0)));

            //Status should be empty because it does not pass time limit
            Assert.Equal(string.Empty, actual);
        }

        private async Task<PingStatus> MockStatus(string status)
        {
            return new PingStatus
            {
                LastCheckDate = DateTimeOffset.Now,
                Status = status
            };
        }
    }
}
