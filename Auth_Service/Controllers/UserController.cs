using Auth_Service.Models.Dtos;
using Auth_Service.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userService;
        private readonly ResponseDto _responseDto;

        public UserController(IUser userService)
        {
            _responseDto = new ResponseDto();
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ResponseDto>> RegisterUser(RegisterUserDto registerUserDto) 
        {
            var response = await _userService.RegisterUser(registerUserDto);
            if (string.IsNullOrWhiteSpace(response)) 
            {
                _responseDto.Result = "User Registered Succesfully!";
                return Created("", _responseDto);
            }
            _responseDto.ErrorMessage = response;
            _responseDto.IsSuccess = false;
            return BadRequest(_responseDto);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ResponseDto>> LoginUser(LoginRequestDto loginRequestDto)
        {
            var response = await _userService.LoginUser(loginRequestDto);
            if (response.User != null)
            {
                _responseDto.Result = response;
                return Created("", _responseDto);
            }
            _responseDto.ErrorMessage = "Invalid Credentials";
            _responseDto.IsSuccess = false;
            return BadRequest(_responseDto);
        }

        [HttpPost("Assign Role")]
        public async Task<ActionResult<ResponseDto>> AssignRole(AssignUserRoleDto assignUserRoleDto)
        {
            var response = await _userService.AssignUserRoles(assignUserRoleDto);
            if (response != null)
            {
                //this was success
                _responseDto.Result = response;
                return Ok(_responseDto);
            }
            _responseDto.ErrorMessage = "An Error Occurred!";
            _responseDto.IsSuccess = false;
            _responseDto.Result = false;
            return BadRequest(_responseDto);
        }
    }
}
