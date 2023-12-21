using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Post_Service.Models;
using Post_Service.Models.Dtos;
using Post_Service.Services.IService;

namespace Post_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPost _postService;
        private readonly IMapper _mapper;
        private readonly ResponseDto _responseDto;

        public PostController(IPost postService, IMapper mapper)
        {
            _mapper = mapper;
            _postService = postService;
            _responseDto = new ResponseDto();
        }

        [HttpGet("All posts")]
        public async Task<ActionResult<ResponseDto>> GetAllPosts()
        {
            try
            {
                var posts = await _postService.GetAllPostsAsync();
                if (posts == null)
                {
                    _responseDto.Result = new ResponseDto();
                    return _responseDto;
                }
                _responseDto.Result = posts;
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("All posts by User {userId}")]
        public async Task<ActionResult<ResponseDto>> GetAllPostsByUserId(Guid userId)
        {
            try
            {
                var posts = await _postService.GetPostOfUser(userId);
                if (posts == null)
                {
                    _responseDto.Result = new ResponseDto();
                    return _responseDto;
                }
                _responseDto.Result = posts;
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Add post")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ResponseDto>> GetAllPostsByUserId(AddPostDto addPostDto)
        {
            try
            {
                var post = _mapper.Map<Post>(addPostDto);
                var response = await _postService.AddPostAsync(post);
                _responseDto.Result = response;
                return Created("", _responseDto);      
             }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost("Update post{id}")]
        //public async Task<ActionResult<ResponseDto>> GetAllPostsByUserId(Guid id, AddPostDto addPostDto)
        //{
        //    try
        //    {
        //        var post = await _postService.GetPostByIdAsync(id);
        //        if (post == null)
        //        {
        //            _responseDto.ErrorMessage = "Post Not Found";
        //            return NotFound(_responseDto);
        //        }

        //        var updatedPost = await _postService.

        //        _responseDto.Result = response;
        //        return Created("", _responseDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
