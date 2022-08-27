using HotelWebApi.Contracts.Dtos;
using HotelWebApi.Contracts.Dtos.Authorization;
using HotelWebApi.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> Index()
        {
            var userDtos = await _userService.GetAll();
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById([FromRoute]int id)
        {
            var userDto = await _userService.GetById(id);
            return Ok(userDto);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateUserDto dto)
        {
            var createdUserId = await _userService.Create(dto);
            return Created($"api/{createdUserId}", dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute]int id, [FromBody]UpdateUserDto dto)
        {
            var updatedUser = await _userService.Update(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var deletedUser = await _userService.Delete(id);
            return NoContent();
        }
    }
}
