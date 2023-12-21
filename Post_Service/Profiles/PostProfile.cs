using AutoMapper;
using Post_Service.Models;
using Post_Service.Models.Dtos;

namespace Post_Service.Profiles
{
    public class PostProfile: Profile
    {
        public PostProfile()
        {
            CreateMap<AddPostDto, Post>().ReverseMap();
        }
    }
}
