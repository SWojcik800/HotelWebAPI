using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;

namespace HotelWebAPI.Tests.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
                /*.WithWebHostBuilder(builder => {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(HotelWebAPIDbContext));
                        services.AddDbContext<HotelWebAPIDbContext>(options =>
                        {
                            options.UseInMemoryDatabase(databaseName: "TestDb");
                        });
                    });
                });*/
            TestClient = appFactory.CreateClient();
        }
    }
}
