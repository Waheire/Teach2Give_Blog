using Auth_Service.Data;
using Auth_Service.Models;
using Auth_Service.Models.Dtos;
using Auth_Service.Services.IService;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth_Service.Services
{
    public class UserService : IUser
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwt _jwtServices;

        public UserService(AppDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwt jwtServices)
        {
            _context = context;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
            _jwtServices = jwtServices;
        }


        public async Task<string> AssignUserRoles(AssignUserRoleDto assignUserRoleDto)
        {
            try 
            {
                //check if username exists
                var user = await _context.ApplicationUsers.Where(u => u.Email.ToLower() == assignUserRoleDto.Email.ToLower()).FirstOrDefaultAsync();
                if (user == null)
                {
                    return "Username does not exist";
                }
                else 
                {
                    //check if role exist
                    var roleExist = await _roleManager.RoleExistsAsync(assignUserRoleDto.RoleName);
                    if (!roleExist) 
                    {
                        //create role
                        await _roleManager.CreateAsync(new IdentityRole(assignUserRoleDto.RoleName));
                    }
                    //asign role
                    await _userManager.AddToRoleAsync(user, assignUserRoleDto.RoleName);
                    return "User succesfully assigned Role!";
                }

            } catch (Exception ex) 
            {
                return ex.Message;
            }
        }

        public async Task<LoginResponseDto> LoginUser(LoginRequestDto loginRequestDto)
        {
            try
            {
                //check if username exists
                var user = await _context.ApplicationUsers.Where(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower()).FirstOrDefaultAsync();
                //check password
                var isValidPassword = await _userManager.CheckPasswordAsync(user, loginRequestDto.password);
                //chcek user if null, check password is valid 
                if (user == null || !isValidPassword ) 
                {
                    return new LoginResponseDto();
                }
                var loggedUser = _mapper.Map<UserResponseDto>(user);
                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtServices.GenerateToken(user, roles);
                var response = new LoginResponseDto() 
                {
                    User = loggedUser,
                    Token = token
                };

                return response;
            } catch (Exception ex) 
            {
                return new LoginResponseDto();
            }
        }

        public async Task<string> RegisterUser(RegisterUserDto registerUserDto)
        {
            try
            {
                var user = _mapper.Map<ApplicationUser>(registerUserDto);

                //create user 
                var response = await _userManager.CreateAsync(user, registerUserDto.password);

                //if succeeded
                if (response.Succeeded)
                {
                    //does the role exist
                    if (!_roleManager.RoleExistsAsync(registerUserDto.Role).GetAwaiter().GetResult())
                    {
                        //create role
                        await _roleManager.CreateAsync(new IdentityRole(registerUserDto.Role));
                    }

                    //assign the user the role
                    await _userManager.AddToRoleAsync(user, registerUserDto.Role);
                    return string.Empty;
                }
                else
                {
                    return response.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
