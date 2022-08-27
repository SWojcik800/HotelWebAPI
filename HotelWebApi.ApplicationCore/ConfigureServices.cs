using FluentValidation;
using HotelWebApi.ApplicationCore.Common.Authorization;
using HotelWebApi.ApplicationCore.Common.Seeders;
using HotelWebApi.ApplicationCore.Common.Validators;
using HotelWebApi.ApplicationCore.Contexts;
using HotelWebApi.ApplicationCore.Entities.Authentication;
using HotelWebApi.ApplicationCore.Features.Authorization;
using HotelWebApi.ApplicationCore.Features.Hotels;
using HotelWebApi.ApplicationCore.Repositories;
using HotelWebApi.ApplicationCore.Validators;
using HotelWebApi.Contracts.Dtos.Authorization;
using HotelWebApi.Contracts.Repositories;
using HotelWebApi.Contracts.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace HotelWebApi.ApplicationCore
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationCore(this IServiceCollection services, IConfiguration configuration)
        {
            var authenticationSettings = new AuthenticationSettings();
            configuration.GetSection("Authentication").Bind(authenticationSettings);

            services.AddSingleton(authenticationSettings);

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
                };
            });


            services.AddMediatR(Assembly.Load("HotelWebApi.ApplicationCore"));
            services.AddDbContext<HotelWebAPIDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("HotelDbConnection")));
            
            services.AddScoped<HotelSeeder>();
            services.AddAutoMapper(Assembly.Load("HotelWebApi.ApplicationCore"));
           

            services.AddScoped<IUserContextService, UserContextService>();

            services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IValidator<CreateUserDto>, CreateUserValidator>();
            services.AddScoped<IValidator<RegisterUserDto>, RegisterUserValidator>();
            services.AddScoped<IValidator<UpdateUserDto>, UpdateUserValidator>();
            services.AddScoped<IValidator<CreateRoleDto>, CreateRoleValidator>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            return services;
        }
    }
}
