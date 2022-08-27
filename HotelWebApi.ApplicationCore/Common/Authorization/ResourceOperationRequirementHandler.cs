using HotelWebApi.ApplicationCore.Entities.ApiData;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HotelWebApi.ApplicationCore.Common.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Hotel>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Hotel hotel)
        {
            string[] allowedRoles = { "Admin", "Moderator" };

            if (requirement.ResourceOperation == ResourceOperation.Read ||
                requirement.ResourceOperation == ResourceOperation.Create
                )
            {
                context.Succeed(requirement);
            }

            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var role = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;

            if (hotel.CreatedById == int.Parse(userId) || allowedRoles.Contains(role))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
