using AutoMapper;
using HotelWebApi.ApplicationCore.Common.Authorization;
using HotelWebApi.ApplicationCore.Common.Exceptions;
using HotelWebApi.ApplicationCore.Entities.Authentication;
using HotelWebApi.Contracts.Dtos.Authorization;
using HotelWebApi.Contracts.Repositories;
using HotelWebApi.Contracts.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HotelWebApi.ApplicationCore.Features.Authorization
{
    public class AccountService : IAccountService
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly ILogger<AccountService> _logger;
        private readonly IMapper _mapper;

        public AccountService(IUserService userService, IUserRepository userRepository, IMapper mapper, IPasswordHasher<User> passwordHasher,
           ILogger<AccountService> logger, AuthenticationSettings authenticationSettings)
        {
            _userService = userService;
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _logger = logger;
        }

        public async Task<string> GenerateJwtToken(LoginDto dto)
        {


            var users = await _userRepository.GetAll();
            var user = users.FirstOrDefault(u => u.Email == dto.Email);

            if (user is null)
            {
                throw new BadRequestException("Invalid login attempt");
            }


            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);


            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid login attempt");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, $"{user.Email}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),

            };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer, claims,
                expires: expires,
                signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);

        }

        public async Task<int> Register(RegisterUserDto dto)
        {
            var createDto = _mapper.Map<CreateUserDto>(dto);
            createDto.RoleId = 1;

            var newUserId = await _userService.Create(createDto);

            return newUserId;
        }
    }
}
