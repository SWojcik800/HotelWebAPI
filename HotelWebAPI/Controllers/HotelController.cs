using HotelWebAPI.Models.Dtos;
using HotelWebAPI.Services;
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

            if (hotelDto is null)
                return NotFound(null);

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
            if (deletedHotel is null)
                return NotFound();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute]int id, [FromBody]UpdateHotelDto dto)
        {
            var updatedHotel = _hotelService.Update(id, dto);
            if (updatedHotel is null)
                return NotFound();

            return NoContent();
        }


    }
}
