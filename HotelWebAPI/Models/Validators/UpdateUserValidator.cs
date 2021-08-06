using FluentValidation;
using HotelWebAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWebAPI.Models.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator()
        {
            RuleFor(u => u.NewPassword)
                .NotEmpty();

            RuleFor(u => u.ConfirmNewPassword)
                .Equal(u => u.NewPassword);
        }
    }
}
