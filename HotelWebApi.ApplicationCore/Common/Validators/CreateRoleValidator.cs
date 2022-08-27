using FluentValidation;
using HotelWebApi.ApplicationCore.Contexts;
using HotelWebApi.Contracts.Dtos.Authorization;
using System.Linq;

namespace HotelWebApi.ApplicationCore.Common.Validators
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleDto>
    {
        public CreateRoleValidator(HotelWebAPIDbContext dbContext)
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(r => r.Name)
                .Custom((value, context) =>
                {
                    var roleExists = dbContext.Roles.Any(r => r.Name.ToLower() == value.ToLower());
                    if (roleExists)
                    {
                        context.AddFailure("Name", "Role with that name alredy exists");
                    }
                });
        }
    }
}
