using Auth_Service.Models;

namespace Auth_Service.Services.IService
{
    public interface IJwt
    {
        string GenerateToken(ApplicationUser user, IEnumerable<string> Roles);
    }
}
