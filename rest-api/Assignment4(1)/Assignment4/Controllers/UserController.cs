using IMDBAPI.Exceptions;
using IMDBAPI.Models.RequestModels;
using IMDBAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IMDBAPI.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) { 
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody]UserRequest user) {
            
            return Ok(new { token = _userService.CreateUser(user) });
        }

        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] UserRequest user)
        {
            if(user.Password is null || user.Email is null)
            {
                throw new InvalidInputException("Invalid Creds");
            }
            return Ok(new { token = _userService.LoginUser(user.Email, user.Password) });
        }
    }
}
