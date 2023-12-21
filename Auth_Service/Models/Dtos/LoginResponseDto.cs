namespace Auth_Service.Models.Dtos
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;

        public UserResponseDto User { get; set; } = default!;
    }
}
