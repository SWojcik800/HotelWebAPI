using AutoMapper;
using HotelWebAPI.Entities.ApiData;
using HotelWebAPI.Exceptions;
using HotelWebAPI.Models.Dtos;
using HotelWebAPI.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWebAPI.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<HotelService> _logger ;

        public HotelService(IHotelRepository hotelRepository, IMapper mapper, ILogger<HotelService> logger)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
            _logger = logger;
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
            var hotelResult = await _hotelRepository.Create(hotel);
            var hotelId = hotelResult.Id;

            _logger.LogInformation($"Hotel with id {hotelId} has been created.");

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
                

            var hotelDto = _mapper.Map<HotelDto>(hotel);
            _logger.LogInformation($"Deleted hotel with id {id}.");

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
                

            hotelToUpdate.Name = dto.Name;
            hotelToUpdate.Description = dto.Description;
            hotelToUpdate.Stars = dto.Stars;

            var hotel = await _hotelRepository.Update(id, hotelToUpdate);

            var hotelDto = _mapper.Map<HotelDto>(hotel);
            _logger.LogInformation($"Hotel with id {id} has been created.");

            return hotelDto;
        }
    }
}
