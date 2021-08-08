using FluentValidation;
using FluentValidation.AspNetCore;
using HotelWebAPI.Entities;
using HotelWebAPI.Middleware;
using HotelWebAPI.Models.Dtos;
using HotelWebAPI.Models.Validators;
using HotelWebAPI.Repositories;
using HotelWebAPI.Seeders;
using HotelWebAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HotelWebAPIDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("HotelDbConnection")));


            services.AddControllers().AddFluentValidation();


            services.AddScoped<HotelSeeder>();
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IValidator<CreateUserDto>, CreateUserValidator>();
            services.AddScoped<IValidator<UpdateUserDto>, UpdateUserValidator>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelWebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, HotelSeeder hotelSeeder)
        {

            hotelSeeder.Seed();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelWebAPI v1"));
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
