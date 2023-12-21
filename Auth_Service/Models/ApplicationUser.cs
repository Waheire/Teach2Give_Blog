using Microsoft.AspNetCore.Identity;

namespace Auth_Service.Models
{
    public class ApplicationUser:IdentityUser 
    {
        public string Name { get; set; } = string.Empty;
    }
}
