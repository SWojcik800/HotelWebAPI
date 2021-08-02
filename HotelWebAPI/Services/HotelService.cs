using AutoMapper;
using HotelWebAPI.Entities.ApiData;
using HotelWebAPI.Models.Dtos;
using HotelWebAPI.Repositories;
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

        public HotelService(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<List<HotelDto>> GetAll()
        {
            var hotels = await _hotelRepository.GetAll();

            var hotelDtos = _mapper.Map<List<HotelDto>>(hotels);

            return hotelDtos;
        }

        public async Task<HotelDto> GetById(int id)
        {
            var hotel = await _hotelRepository.GetById(id);

            if (hotel is null)
                return null;

            var hotelDto = _mapper.Map<HotelDto>(hotel);

            return hotelDto;
        }

        public async Task<HotelDto> Create(CreateHotelDto dto)
        {
            if (dto is null)
                return null;

            var hotel = _mapper.Map<Hotel>(dto);
            var hotelResult = await _hotelRepository.Create(hotel);
            var hotelDto = _mapper.Map<HotelDto>(hotelResult);

            return hotelDto;

        }

        public async Task<HotelDto> Delete(int id)
        {

            var hotel = await _hotelRepository.Delete(id);

            if (hotel is null)
                return null;

            var hotelDto = _mapper.Map<HotelDto>(hotel);

            return hotelDto;

        }

        public async Task<HotelDto> Update(int id, UpdateHotelDto dto)
        {
            var hotelToUpdate = await _hotelRepository.GetById(id);

            if (hotelToUpdate == null)
                return null;

            hotelToUpdate.Name = dto.Name;
            hotelToUpdate.Description = dto.Description;
            hotelToUpdate.Stars = dto.Stars;

            var hotel = await _hotelRepository.Update(id, hotelToUpdate);

            var hotelDto = _mapper.Map<HotelDto>(hotel);

            return hotelDto;
        }
    }
}
