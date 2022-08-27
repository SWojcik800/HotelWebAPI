﻿namespace HotelWebApi.Contracts.Dtos.Authorization
{
    public class RegisterUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public int RoleId { get; set; } = 1;
    }
}
