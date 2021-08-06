using FluentValidation;
using HotelWebAPI.Entities;
using HotelWebAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWebAPI.Models.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserValidator(HotelWebAPIDbContext dbContext)
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.Password)
                .MinimumLength(6);

            RuleFor(u => u.ConfirmPassword)
                .Equal(u => u.Password);

            RuleFor(u => u.Email)
                .Custom((value, context) =>
                {
                    var emailIsInUse = dbContext.Users.Any(u => u.Email == value);
                    if (emailIsInUse)
                    {
                        context.AddFailure("Email", "Email must be unique");
                    }

                });

            RuleFor(u => u.RoleId)
                .NotEmpty()
                .NotNull();

            RuleFor(u => u.RoleId)
                .Custom((value, context) => {
                    var roleExists = dbContext.Roles.Any(r => r.Id == value);
                    if (!roleExists)
                    {
                        context.AddFailure("RoleId", "Role with that id does not exist");
                    }

                });
        }
    }
}
