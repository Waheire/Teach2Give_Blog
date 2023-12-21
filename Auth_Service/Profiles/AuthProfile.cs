using Auth_Service.Models;
using Auth_Service.Models.Dtos;
using AutoMapper;

namespace Auth_Service.Profiles
{
    public class AuthProfile:Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterUserDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, src => src.MapFrom(u => u.Email));

            CreateMap<UserResponseDto, ApplicationUser>().ReverseMap();
        }
    }
}
