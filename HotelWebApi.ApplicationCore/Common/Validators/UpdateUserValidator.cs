using FluentValidation;
using HotelWebApi.Contracts.Dtos.Authorization;

namespace HotelWebApi.ApplicationCore.Common.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator()
        {
            RuleFor(u => u.NewPassword)
                .NotEmpty()
                .MinimumLength(6)
                .Matches("[A-Z]").WithMessage("Password must contain at least one capital letter.")
                .Matches("[0-9]").WithMessage("Password must contain a number");

            RuleFor(u => u.ConfirmNewPassword)
                .Equal(u => u.NewPassword);
        }
    }
}
