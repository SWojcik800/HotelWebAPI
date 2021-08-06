using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Xunit.Assert;

namespace HotelWebAPI.Tests.IntegrationTests
{
    
    public class HotelControllerTests : IntegrationTest
    {
        public HotelControllerTests(): base()
        {

        }

        [Theory]
        [InlineData("/api/Hotel")]
        public async Task GetAll_Endpoint_ReturnsSuccessStatusCode(string url)
        {
            var response = await TestClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
        }
    }
}
