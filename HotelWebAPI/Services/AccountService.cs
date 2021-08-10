using AutoMapper;
using HotelWebAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWebAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AccountService(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<int> Register(RegisterUserDto dto)
        {
            var createDto = _mapper.Map<CreateUserDto>(dto);
            var newUserId = await _userService.Create(createDto);

            return newUserId;
        }
    }
}
