using AutoMapper;
using HotelWebApi.ApplicationCore.Common.Exceptions;
using HotelWebApi.ApplicationCore.Entities.Authentication;
using HotelWebApi.Contracts.Dtos.Authorization;
using HotelWebApi.Contracts.Repositories;
using HotelWebApi.Contracts.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelWebApi.ApplicationCore.Features.Authorization
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RoleService> _logger;

        public RoleService(IRoleRepository roleRepository, IMapper mapper, ILogger<RoleService> logger)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<RoleDto>> GetAll()
        {
            _logger.LogInformation("GetAll action invoked");

            var roles = await _roleRepository.GetAll();
            var roleDtos = _mapper.Map<List<RoleDto>>(roles);

            return roleDtos;
        }

        public async Task<RoleDto> GetById(int id)
        {
            _logger.LogInformation("GetById action invoked");

            var role = await _roleRepository.GetById(id);

            if (role is null)
            {
                _logger.LogError($"Role with id {id} could not be found");
                throw new NotFoundException($"Role with id {id} could not be found");
            }

            var roleDto = _mapper.Map<RoleDto>(role);

            return roleDto;
        }

        public async Task<RoleDto> Create(CreateRoleDto dto)
        {
            var role = _mapper.Map<Role>(dto);
            var createdRole = await _roleRepository.Create(role);

            if (createdRole is null)
            {

                throw new Exception("Couldn't create a role");
            }

            var createdRoleDto = _mapper.Map<RoleDto>(createdRole);

            _logger.LogInformation($"Created new role with id {createdRole.Id}");
            return createdRoleDto;
        }

        public async Task<RoleDto> Delete(int id)
        {
            var deletedRole = await _roleRepository.Delete(id);

            if (deletedRole is null)
            {
                _logger.LogError($"Role with id {id} cold not be found");
                throw new NotFoundException($"Role with id {id} cold not be found");
            }

            _logger.LogInformation($"Role with id {id} has been deleted");
            var dto = _mapper.Map<RoleDto>(deletedRole);
            return dto;
        }

        public async Task<RoleDto> Update(int id, CreateRoleDto dto)
        {


            var role = await _roleRepository.GetById(id);
            role.Name = dto.Name;

            var updatedRole = await _roleRepository.Update(id, role);

            if (updatedRole is null || dto is null)
            {
                _logger.LogError($"Role with id {id} could not be found");
                throw new NotFoundException($"Role with id {id} could not be found");
            }



            _logger.LogInformation($"Role with id {id} has been created");
            var updatedDto = _mapper.Map<RoleDto>(updatedRole);

            return updatedDto;
        }
    }
}
