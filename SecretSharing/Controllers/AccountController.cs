using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecretSharing.Core.Entities.Identity;
using SecretSharing.Core.Interfaces;
using SecretSharing.Dtos;
using SecretSharing.Errors;

namespace SecretSharing.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : BaseApiController
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenServices;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ITokenService tokenServices)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenServices = tokenServices;
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistAsync([FromQuery] string Email)
        {
            return await _userManager.FindByEmailAsync(Email) != null;
        }

        [HttpPost(nameof(Register))]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (CheckEmailExistAsync(registerDto.Email).Result.Value)
            {
                return BadRequest(new APIValidationErrorResponse { Errors = new[] { "Email is already in use" } });
            }
            var user = new AppUser
            {
                Email = registerDto.Email,
                UserName = registerDto.Email
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(new APIResponse(400));
            return new UserDto
            {
                Email = user.Email,
                Token = _tokenServices.GenerateToken(user)
            };
        }

        [HttpPost(nameof(Login))]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized(new APIResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized(new APIResponse(401));
            return new UserDto
            {
                Email = user.Email,
                Token = _tokenServices.GenerateToken(user)
            };
        }
    }
}
