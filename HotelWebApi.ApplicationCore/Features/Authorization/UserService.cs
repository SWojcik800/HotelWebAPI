using AutoMapper;
using HotelWebApi.ApplicationCore.Common.Exceptions;
using HotelWebApi.ApplicationCore.Entities.Authentication;
using HotelWebApi.Contracts.Dtos;
using HotelWebApi.Contracts.Dtos.Authorization;
using HotelWebApi.Contracts.Repositories;
using HotelWebApi.Contracts.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelWebApi.ApplicationCore.Features.Authorization
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher<User> passwordHasher,
            ILogger<UserService> logger
            )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        public async Task<List<UserDto>> GetAll()
        {
            var users = await _userRepository.GetAll();
            _logger.LogInformation("Invoked GetAll action");


            if (users is null)
                return null;

            var userDtos = _mapper.Map<List<UserDto>>(users);
            return userDtos;
        }

        public async Task<UserDto> GetById(int id)
        {
            _logger.LogInformation("Invoked GetById action");
            var user = await _userRepository.GetById(id);
            var userDto = _mapper.Map<UserDto>(user);

            if (user is null)
            {
                _logger.LogInformation($"User with id {id} was not found");
                throw new NotFoundException($"User with id {id} was not found");
            }



            return userDto;
        }

        public async Task<int> Create(CreateUserDto dto)
        {

            var user = new User()
            {
                Email = dto.Email,
                RoleId = dto.RoleId
            };

            var hashedPassword = _passwordHasher.HashPassword(user, dto.Password);
            user.PasswordHash = hashedPassword;

            var createdUser = await _userRepository.Create(user);

            if (createdUser is null)
            {
                _logger.LogError("Failed to create a user");
                throw new Exception("Internal server error");
            }


            _logger.LogInformation($"User with id {createdUser.Id} has been created");

            return createdUser.Id;


        }

        public async Task<UserDto> Delete(int id)
        {
            var user = await _userRepository.Delete(id);

            if (user is null)
            {
                _logger.LogWarning($"Failed to delete user with id: {id}");
                throw new NotFoundException($"User with id {id} was not found.");
            }

            _logger.LogWarning($"Deleted user with id: {id}");
            var userDto = _mapper.Map<UserDto>(user);


            return userDto;
        }

        public async Task<UserDto> Update(int id, UpdateUserDto dto)
        {
            var user = await _userRepository.GetById(id);

            if (user is null || dto is null)
            {
                _logger.LogWarning($"Failed to update user with id: {id}");
                throw new NotFoundException($"User with id {id} was not found.");
            }

            if (dto.NewPassword != dto.ConfirmNewPassword)
            {
                _logger.LogWarning($"Failed to update user with id: {id}");
                throw new BadRequestException("Passwords does not match");
            }


            var hashedPassword = _passwordHasher.HashPassword(user, dto.NewPassword);
            user.PasswordHash = hashedPassword;

            var updatedUser = await _userRepository.Update(id, user);
            var updatedUserDto = _mapper.Map<UserDto>(updatedUser);

            _logger.LogInformation($"Updated user user with id: {id}");

            return updatedUserDto;
        }
    }
}
