using HotelWebApi.Contracts.Dtos;
using HotelWebApi.Contracts.Services;
using HotelWebAPI.Controllers;
using HotelWebAPI.Tests.Services.Seeders;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Xunit.Assert;

namespace HotelWebAPI.Tests.Controllers
{
    public class HotelControllerTests
    {
        private HotelController _sut;
        private Mock<IHotelService> _hotelService = new Mock<IHotelService>();
        public HotelControllerTests()
        {
            _sut = new HotelController(_hotelService.Object);

            _hotelService
                .Setup(h => h.GetAll())
                .ReturnsAsync(HotelSeeder.GetHotelDtos());

            _hotelService
                .Setup(h => h.GetById(It.IsAny<int>()))
                .ReturnsAsync(HotelSeeder.GetHotelDtos()[0]);

            _hotelService
                .Setup(h => h.Create(It.IsAny<CreateHotelDto>()))
                .ReturnsAsync(HotelSeeder.GetHotels()[0].Id);

            
        }

        [Fact]
        public async Task Index_ReturnsHotelDtos()
        {
            var result = await _sut.Index();
            

            IsType<ActionResult<List<HotelDto>>>(result);
            

        }

        [Fact]
        public async Task Get_ReturnsHotelDtoIfExists()
        {
            var id = HotelSeeder.GetHotels()[0].Id;
            var result = await _sut.Get(id);

            IsType<ActionResult<HotelDto>>(result);
            
        }

        



        [Fact]
        public async Task Create_ReturnsCreatedHotelDto()
        {
            var hotelToCreate = HotelSeeder.GetCreatedHotelDto();

            var result = await _sut.Create(hotelToCreate);
            IsType<CreatedResult>(result);

        }

        [Fact]
        public async Task Delete_DeletesHotelDtoIfExists()
        {
            _hotelService
                .Setup(h => h.Delete(It.IsAny<int>()))
                .ReturnsAsync(HotelSeeder.GetHotelDtos()[0]);

                
            var id = 1;
            var result = await _sut.Delete(id);

            IsType<NoContentResult>(result);
        }

        

        [Fact]
        public async Task Update_UpdatesHotelDtoIfExists()
        {
            _hotelService
                .Setup(h => h.Update(It.IsAny<int>(), It.IsAny<UpdateHotelDto>()))
                .ReturnsAsync(HotelSeeder.GetHotelDtos()[0]);

            var id = 1;
            var hotelToUpdate = HotelSeeder.GetUpdateHotelDto();
            var result = await _sut.Update(id, hotelToUpdate);

            IsType<NoContentResult>(result);
        }


    }
}
