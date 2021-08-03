using AutoMapper;
using HotelWebAPI.Entities;
using HotelWebAPI.Entities.ApiData;
using HotelWebAPI.Exceptions;
using HotelWebAPI.Models.Dtos;
using HotelWebAPI.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        public UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<List<UserDto>> GetAll()
        {
            var users = await _userRepository.GetAll();

            if (users is null)
                return null;

            var userDtos = _mapper.Map<List<UserDto>>(users);
            return userDtos;
        }

        public async Task<UserDto> GetById(int id)
        {
            var user = await _userRepository.GetById(id);

            if (user is null)
                return null;

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<int> Create(CreateUserDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
                throw new BadRequestException("Passwords does not match");

            var user = new User()
            {
                Email = dto.Email,
                PasswordHash = dto.Password,
                Role = new Role()
                {
                    Name = "User"
                }
            };

            var hashedPassword = _passwordHasher.HashPassword(user, user.PasswordHash);
            user.PasswordHash = hashedPassword;

            var createdUser = await _userRepository.Create(user);

            if (createdUser is null)
                throw new Exception("Internal server error");
            

            return createdUser.Id;


        }

        public async Task<UserDto> Delete(int id)
        {
            var user = await _userRepository.Delete(id);

            if (user is null)
                throw new NotFoundException($"User with id {id} was not found.");

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public async Task<UserDto> Update(int id, UpdateUserDto dto)
        {
            var user = await _userRepository.GetById(id);

            if (user is null || dto is null)
                throw new NotFoundException($"User with id {id} was not found.");
            if (dto.NewPassword != dto.ConfirmNewPassword)
                throw new BadRequestException("Passwords does not match");

            var hashedPassword = _passwordHasher.HashPassword(user, dto.NewPassword);
            user.PasswordHash = hashedPassword;

            var updatedUser = await _userRepository.Update(id, user);
            var updatedUserDto = _mapper.Map<UserDto>(updatedUser);

            return updatedUserDto;
        }
    }
}
