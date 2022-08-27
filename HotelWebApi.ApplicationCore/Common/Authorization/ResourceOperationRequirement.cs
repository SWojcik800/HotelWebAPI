using Microsoft.AspNetCore.Authorization;

namespace HotelWebApi.ApplicationCore.Common.Authorization
{
    public enum ResourceOperation
    {
        Create,
        Update,
        Read,
        Delete
    }

    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperationRequirement(ResourceOperation resourceOperation)
        {
            ResourceOperation = resourceOperation;
        }
        public ResourceOperation ResourceOperation { get; }
    }
}
