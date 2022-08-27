using HotelWebApi.Contracts.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HotelWebApi.ApplicationCore.Features.Authorization
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;
        public int? GetUserId => User is null ? null : int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
        public string GetRole => User.FindFirst(c => c.Type == ClaimTypes.Role).Value;
        public string GetEmail => User.FindFirst(c => c.Type == ClaimTypes.Email).Value;
    }
}
