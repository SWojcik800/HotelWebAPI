using HotelWebApi.Contracts.Dtos;
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
    [Authorize]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;

        }

        [HttpGet]
        public async Task<ActionResult<List<HotelDto>>> Index()
        {
            var hotelDtos = await _hotelService.GetAll();
            return Ok(hotelDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> Get([FromRoute]int id)
        {
            var hotelDto = await _hotelService.GetById(id);

            return Ok(hotelDto);
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]CreateHotelDto dto)
        {
            var createdHotelId = await _hotelService.Create(dto);

            return Created($"/api/hotel/{createdHotelId}", null);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var deletedHotel = await _hotelService.Delete(id);
            
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute]int id, [FromBody]UpdateHotelDto dto)
        {
            var updatedHotel = await _hotelService.Update(id, dto);
            

            return NoContent();
        }


    }
}
