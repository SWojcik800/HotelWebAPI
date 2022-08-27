using FluentValidation;
using HotelWebApi.ApplicationCore.Contexts;
using HotelWebApi.Contracts.Dtos.Authorization;
using System.Linq;

namespace HotelWebApi.ApplicationCore.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserValidator(HotelWebAPIDbContext dbContext)
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.Password)
                .MinimumLength(6)
                .Matches("[A-Z]").WithMessage("Password must contain at least one capital letter.")
                .Matches("[0-9]").WithMessage("Password must contain a number");


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
                .Custom((value, context) =>
                {
                    var roleExists = dbContext.Roles.Any(r => r.Id == value);
                    if (!roleExists)
                    {
                        context.AddFailure("RoleId", "Role with that id does not exist");
                    }

                });
        }
    }
}
