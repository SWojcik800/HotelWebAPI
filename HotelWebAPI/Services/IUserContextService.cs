using System.Security.Claims;

namespace HotelWebAPI.Services
{
    public interface IUserContextService
    {
        string GetRole { get; }
        int? GetUserId { get; }
        ClaimsPrincipal User { get; }

        string GetEmail { get; }
    }
}