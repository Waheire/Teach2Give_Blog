using System.ComponentModel.DataAnnotations;

namespace Auth_Service.Models.Dtos
{
    public class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string password { get; set; } = string.Empty;
    }
}
