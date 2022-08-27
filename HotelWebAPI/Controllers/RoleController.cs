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
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<RoleDto>>> GetAll()
        {
            var roleDtos = await _roleService.GetAll();
            return Ok(roleDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetById([FromRoute]int id)
        {
            var roleDto = await _roleService.GetById(id);
            return Ok(roleDto);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]CreateRoleDto dto)
        {
            var roleDto = await _roleService.Create(dto);
            return Created($"api/Role/{roleDto.Id}", roleDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute]int id)
        {
            await _roleService.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute]int id, [FromBody]CreateRoleDto dto)
        {
            var roleDto = await _roleService.Update(id, dto);
            return Ok(roleDto);
        }

    }
}
