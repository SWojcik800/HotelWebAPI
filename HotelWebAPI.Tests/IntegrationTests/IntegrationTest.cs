using HotelWebAPI.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
