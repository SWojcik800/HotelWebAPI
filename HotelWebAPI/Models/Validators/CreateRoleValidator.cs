using FluentValidation;
using HotelWebAPI.Entities;
using HotelWebAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWebAPI.Models.Validators
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleDto>
    {
        public CreateRoleValidator(HotelWebAPIDbContext dbContext)
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(r => r.Name)
                .Custom((value, context) => {
                    var roleExists = dbContext.Roles.Any(r => r.Name.ToLower() == value.ToLower());
                    if (roleExists)
                    {
                        context.AddFailure("Name", "Role with that name alredy exists");
                    }
                });
        }
    }
}
