using AutoMapper;
using HotelWebApi.ApplicationCore.Common.Authorization;
using HotelWebApi.ApplicationCore.Common.Exceptions;
using HotelWebApi.ApplicationCore.Entities.ApiData;
using HotelWebApi.Contracts.Dtos;
using HotelWebApi.Contracts.Repositories;
using HotelWebApi.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelWebApi.ApplicationCore.Features.Hotels
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<HotelService> _logger;
        private readonly IUserContextService _userContextService;
        private readonly IAuthorizationService _authorizationService;

        public HotelService(IHotelRepository hotelRepository, IMapper mapper, ILogger<HotelService> logger, IUserContextService userContextService, IAuthorizationService authorizationService)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
            _logger = logger;
            _userContextService = userContextService;
            _authorizationService = authorizationService;
        }

        public async Task<List<HotelDto>> GetAll()
        {
            _logger.LogInformation("GetAll action Invoked.");
            var hotels = await _hotelRepository.GetAll();

            var hotelDtos = _mapper.Map<List<HotelDto>>(hotels);


            return hotelDtos;
        }

        public async Task<HotelDto> GetById(int id)
        {
            _logger.LogInformation("GetById action Invoked.");
            var hotel = await _hotelRepository.GetById(id);

            if (hotel is null)
            {
                _logger.LogInformation($"Hotel with id {id} was not found");
                throw new NotFoundException($"Hotel with id {id} was not found.");
            }


            var hotelDto = _mapper.Map<HotelDto>(hotel);



            return hotelDto;
        }

        public async Task<int?> Create(CreateHotelDto dto)
        {
            if (dto is null)
            {
                _logger.LogError("Failed to create a hotel.");
                throw new Exception("Internal server error");
            }


            var hotel = _mapper.Map<Hotel>(dto);
            hotel.CreatedById = _userContextService.GetUserId;

            var hotelResult = await _hotelRepository.Create(hotel);
            var hotelId = hotelResult.Id;

            _logger.LogInformation($"Hotel with id {hotelId} has been created by user with email: {_userContextService.GetEmail}");

            return hotelId;

        }

        public async Task<HotelDto> Delete(int id)
        {

            var hotel = await _hotelRepository.Delete(id);

            if (hotel is null)
            {
                _logger.LogInformation($"Hotel with id {id} was not found.");
                throw new NotFoundException($"Hotel with id {id} was not found.");
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(_userContextService.User, hotel,
                new ResourceOperationRequirement(ResourceOperation.Delete));

            if (!authorizationResult.Succeeded)
            {
                var email = _userContextService.GetEmail;
                throw new ForbidException($"User with email: {email} tried to perform unauthorized delete action");
            }


            var hotelDto = _mapper.Map<HotelDto>(hotel);
            _logger.LogInformation($"Deleted hotel with id {id} by user with email: {_userContextService.GetEmail}");

            return hotelDto;

        }

        public async Task<HotelDto> Update(int id, UpdateHotelDto dto)
        {
            var hotelToUpdate = await _hotelRepository.GetById(id);


            if (hotelToUpdate == null)
            {
                _logger.LogInformation($"Hotel with id {id} was not found.");
                throw new NotFoundException($"Hotel with id {id} was not found.");
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(_userContextService.User, hotelToUpdate,
                new ResourceOperationRequirement(ResourceOperation.Update));


            if (!authorizationResult.Succeeded)
            {
                var email = _userContextService.GetEmail;
                throw new ForbidException($"User with email: {email} tried to perform unauthorized update action");
            }



            hotelToUpdate.Name = dto.Name;
            hotelToUpdate.Description = dto.Description;
            hotelToUpdate.Stars = dto.Stars;
            hotelToUpdate.CreatedById = _userContextService.GetUserId;

            var hotel = await _hotelRepository.Update(id, hotelToUpdate);

            var hotelDto = _mapper.Map<HotelDto>(hotel);
            _logger.LogInformation($"Hotel with id {id} has been updated by user with email: {_userContextService.GetEmail}");

            return hotelDto;
        }
    }
}
