using ExpenseTracker.Case.CoreLayer.DTOs.User;
using ExpenseTracker.Case.CoreLayer.Interfaces.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Case.WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRegisterService _userRegisterManager;
        private readonly IUserLoginService _userLoginManager;
        public UserController(IUserRegisterService userRegisterManager, IUserLoginService userLoginManager)
        {
            _userRegisterManager = userRegisterManager;
            _userLoginManager = userLoginManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            try
            {
                var result = await _userRegisterManager.RegisterAsync(userRegisterDto);

                if (result.Succeeded)
                    return Created("", result);
            }
            catch (Exception)
            {

                throw;
            }
           
             return BadRequest();
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponseDto>> Login([FromBody] UserLoginDto userLoginrDto)
        {
            try
            {
                var loginResponseDto = await _userLoginManager.LoginAsync(userLoginrDto);
                return Ok(loginResponseDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
