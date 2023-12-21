using Auth_Service.Models.Dtos;

namespace Auth_Service.Services.IService
{
    public interface IUser
    {
        Task<string> RegisterUser(RegisterUserDto registerUserDto);

        Task<LoginResponseDto> LoginUser(LoginRequestDto loginRequestDto);

        Task<string> AssignUserRoles(AssignUserRoleDto assignUserRoleDto);

    }
}
