namespace HotelWebApi.Contracts.Dtos.Authorization
{
    public class UpdateUserDto
    {
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
