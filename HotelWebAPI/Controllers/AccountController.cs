using HotelWebApi.Contracts.Dtos.Authorization;
using HotelWebApi.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;


        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody]RegisterUserDto dto)
        {
            var registeredUserId = await _accountService.Register(dto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody]LoginDto dto)
        {
            var token = await _accountService.GenerateJwtToken(dto);
            return Ok(token);
        }
    }
}
