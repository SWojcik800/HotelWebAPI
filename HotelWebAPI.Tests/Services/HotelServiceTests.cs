using AutoMapper;
using HotelWebAPI.Entities.ApiData;
using HotelWebAPI.Models.Dtos;
using HotelWebAPI.Repositories;
using HotelWebAPI.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Xunit.Assert;

namespace HotelWebAPI.Tests.Services
{
    public class HotelServiceTests
    {
        private HotelService _sut;
        private Mock<IHotelRepository> _hotelRepository = new Mock<IHotelRepository>();
        private Mock<IMapper> _mapper = new Mock<IMapper>();

        public HotelServiceTests()
        {
            _sut = new HotelService(_hotelRepository.Object, _mapper.Object);

        }

        private List<Hotel> getMockHotels()
        {
            return new List<Hotel>()
            {
                new Hotel()
                {
                    Name = "hotel 1",
                    Description = "hotel 1 desc",
                    Stars = 3,
                    ContactEmail = "mock@email.com",
                    PhoneNumber = "192 168 001",
                    Address = new Address()
                    {
                        City = "Generic city 1",
                        Street = "Generic street",
                        ZipCode = "34-777"
                    }
                },
                new Hotel()
                {
                    Name = "hotel 2",
                    Description = "hotel 2 desc",
                    Stars = 4,
                    ContactEmail = "mock2@email.com",
                    PhoneNumber = "192 168 002",
                    Address = new Address()
                    {
                        City = "Generic city 1",
                        Street = "Generic street",
                        ZipCode = "34-778"
                    }
                },
                new Hotel()
                {
                    Name = "hotel 3",
                    Description = "hotel 3 desc",
                    Stars = 5,
                    ContactEmail = "mock3@email.com",
                    PhoneNumber = "192 168 003",
                    Address = new Address()
                    {
                        City = "Generic city 1",
                        Street = "Generic street",
                        ZipCode = "34-779"
                    }
                },
            };
            

        }

        private List<HotelDto> getMockHotelDtos()
        {
            return new List<HotelDto>()
            {
                new HotelDto()
            {
                Name = "hotel 4",
                Description = "hotel 4 desc",
                Stars = 5,
                City = "Generic city 4",
                Street = "Generic street",
                ZipCode = "34-780"

            },
                new HotelDto()
            {
                Name = "hotel 4",
                Description = "hotel 4 desc",
                Stars = 5,
                City = "Generic city 4",
                Street = "Generic street",
                ZipCode = "34-780"

            },
                new HotelDto()
            {
                Name = "hotel 4",
                Description = "hotel 4 desc",
                Stars = 5,
                City = "Generic city 4",
                Street = "Generic street",
                ZipCode = "34-780"

            }
        };
        }

        [Fact]
        public async Task GetAll_ReturnsHotelDtos()
        {
            var hotels = getMockHotels();

            var hotelDtos = getMockHotelDtos();

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

            var mockHotel = new Hotel()
            {
                Id = 1,
                Name = "hotel 1",
                Description = "hotel 1 desc",
                Stars = 3,
                ContactEmail = "mock@email.com",
                PhoneNumber = "192 168 001",
                Address = new Address()
                {
                    City = "Generic city 1",
                    Street = "Generic street",
                    ZipCode = "34-777"
                }
            };

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

        private CreateHotelDto GetCreateHotelDto()
        {
            return new CreateHotelDto()
            {
                Name = "hotel 4",
                Description = "hotel 4 desc",
                Stars = 3,
                PhoneNumber = "192 168 001",
                ContactEmail = "Generic contact email",
                City = "Generic city 4",
                Street = "Generic street",
                ZipCode = "34-780"
            };
        }

        [Fact]

        public async Task Create_ReturnsCreatedHotelDto()
        {
            var mockHotel = getMockHotels()[0];
            var mockHotelDto = getMockHotelDtos()[0];
            var mockCreateHotelDto = GetCreateHotelDto();

            _hotelRepository
                .Setup(h => h.GetById(It.IsAny<int>()))
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

            IsType<HotelDto>(result);
            Equal("hotel 4", result.Name);

        }

        [Fact]

        public async Task Delete_ReturnsDeletedHotelDtoIfExists()
        {
            var mockHotel = getMockHotels()[0];
            var mockHotelDto = getMockHotelDtos()[0];

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

        public async Task Update_ReturnsUpdatedHotelDto()
        {
            var mockHotel = getMockHotels()[0];
            var mockHotelDto = getMockHotelDtos()[0];

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


    }
}
