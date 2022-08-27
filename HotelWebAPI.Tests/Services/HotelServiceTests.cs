using AutoMapper;
using HotelWebApi.ApplicationCore.Common.Exceptions;
using HotelWebApi.ApplicationCore.Entities.ApiData;
using HotelWebApi.ApplicationCore.Features.Hotels;
using HotelWebApi.Contracts.Dtos;
using HotelWebApi.Contracts.Repositories;
using HotelWebApi.Contracts.Services;
using HotelWebAPI.Tests.Services.Seeders;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using static Xunit.Assert;

namespace HotelWebAPI.Tests.Services
{
    public class HotelServiceTests
    {
        private readonly HotelService _sut;
        private readonly Mock<IHotelRepository> _hotelRepository = new Mock<IHotelRepository>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly Mock<ILogger<HotelService>> _logger = new Mock<ILogger<HotelService>>();
        private readonly Mock<IUserContextService> _userContextService = new Mock<IUserContextService>();
        private readonly MockIAuthorizationService _authorizationService = new MockIAuthorizationService();

        public HotelServiceTests()
        {
            _sut = new HotelService(_hotelRepository.Object, _mapper.Object, _logger.Object, _userContextService.Object, _authorizationService);

        }

        

        

        [Fact]
        public async Task GetAll_ReturnsHotelDtos()
        {
            var hotels = HotelSeeder.GetHotels();

            var hotelDtos = HotelSeeder.GetHotelDtos();

            _hotelRepository
                .Setup(h => h.GetAll())
                .ReturnsAsync(hotels);
            

            _mapper.Setup(m => m.Map<List<HotelDto>>(It.IsAny<List<Hotel>>()))
                .Returns(hotelDtos);

           

            var result = await _sut.GetAll();

            IsType<List<HotelDto>>(result);
            NotEmpty(result);
            
        }

        [Fact]
        public async Task GetById_ReturnsHotelDtoIfExists()
        {

            var mockHotel = HotelSeeder.GetHotels()[0];

            var mockHotelDto = new HotelDto()
            {
                Name = "hotel 4",
                Description = "hotel 4 desc",
                Stars = 3,
                City = "Generic city 4",
                Street = "Generic street",
                ZipCode = "34-780"

            };


            _hotelRepository
                .Setup(h => h.GetById(It.IsAny<int>()))
                .ReturnsAsync(mockHotel);

            _mapper
                .Setup(m => m.Map<HotelDto>(It.IsAny<Hotel>()))
                .Returns(mockHotelDto);

            var result = await _sut.GetById(1);

            IsType<HotelDto>(result);
            Equal(3, result.Stars);

        }

        [Fact]
        public async Task GetById_ThrowsNotFoundExceptionIfHotelDoesNotExist()
        {
            var mockHotel = HotelSeeder.GetHotels()[0];


            _hotelRepository
                .Setup(h => h.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            Func<Task> act = () => _sut.GetById(mockHotel.Id);

            await ThrowsAsync<NotFoundException>(act);
        }




        [Fact]

        public async Task Create_ReturnsCreatedHotelId()
        {
            var mockHotel = HotelSeeder.GetHotels()[0];
            mockHotel.Id = 1;

            var mockHotelDto = HotelSeeder.GetHotelDtos()[0];
            var mockCreateHotelDto = HotelSeeder.GetCreatedHotelDto();

            _hotelRepository
                .Setup(h => h.GetById(It.IsAny<int>()))
                .ReturnsAsync(mockHotel);

            _hotelRepository
                .Setup(h => h.Create(It.IsAny<Hotel>()))
                .ReturnsAsync(mockHotel);
                

            _mapper
               .Setup(m => m.Map<CreateHotelDto>(It.IsAny<Hotel>()))
               .Returns(mockCreateHotelDto);

            _mapper
               .Setup(m => m.Map<HotelDto>(It.IsAny<Hotel>()))
               .Returns(mockHotelDto);

            _mapper
               .Setup(m => m.Map<Hotel>(It.IsAny<CreateHotelDto>()))
               .Returns(mockHotel);


            var result = await _sut.Create(mockCreateHotelDto);

            IsType<int>(result);
            

        }


        

        [Fact]

        public async Task Delete_ReturnsDeletedHotelDtoIfExists()
        {
            var mockHotel = HotelSeeder.GetHotels()[0];
            var mockHotelDto = HotelSeeder.GetHotelDtos()[0];

            

            _hotelRepository
                .Setup(h => h.Delete(It.IsAny<int>()))
                .ReturnsAsync(mockHotel);

            _mapper
               .Setup(m => m.Map<HotelDto>(It.IsAny<Hotel>()))
               .Returns(mockHotelDto);

            _mapper
               .Setup(m => m.Map<Hotel>(It.IsAny<CreateHotelDto>()))
               .Returns(mockHotel);

            

            var result = await _sut.Delete(mockHotel.Id);

            IsType<HotelDto>(result);
            Equal("hotel 4", result.Name);
        }

        [Fact]
        public async Task Delete_ThrowsNotFoundExceptionIfHotelDoesNotExist()
        {
            var mockHotel = HotelSeeder.GetHotels()[0];

            _hotelRepository
                .Setup(h => h.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            Func<Task> act = () => _sut.Delete(mockHotel.Id);

            await ThrowsAsync<NotFoundException>(act);
        }


        [Fact]

        public async Task Update_ReturnsUpdatedHotelDto()
        {
            var mockHotel = HotelSeeder.GetHotels()[0];
            var mockHotelDto = HotelSeeder.GetHotelDtos()[0];

            mockHotelDto.Name = "Updated name";

            var mockUpdateHotelDto = new UpdateHotelDto()
            {
                Name = "Updated name",
                Description = "Updated description",
                Stars = 6
            };

            _hotelRepository
                .Setup(h => h.Update(It.IsAny<int>(), It.IsAny<Hotel>() ))
                .ReturnsAsync(mockHotel);

            _hotelRepository
                .Setup(h => h.GetById(It.IsAny<int>()))
                .ReturnsAsync(mockHotel);

            _mapper
               .Setup(m => m.Map<HotelDto>(It.IsAny<Hotel>()))
               .Returns(mockHotelDto);

            var result = await _sut.Update(mockHotel.Id, mockUpdateHotelDto);

            IsType<HotelDto>(result);
            Equal("Updated name", result.Name);

            

        }

        [Fact]
        public async Task Update_ThrowsNotFoundExceptionIfHotelDoesNotExist()
        {
            var mockHotel = HotelSeeder.GetHotels()[0];
            var mockUpdateHotelDto = HotelSeeder.GetUpdateHotelDto();
         

            _hotelRepository
                .Setup(h => h.Update(It.IsAny<int>(), It.IsAny<Hotel>()))
                .ReturnsAsync(() => null);

            Func<Task> act = () => _sut.Update(mockHotel.Id, mockUpdateHotelDto);

            await ThrowsAsync<NotFoundException>(act);
        }


    }
}
