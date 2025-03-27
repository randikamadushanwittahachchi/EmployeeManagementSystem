using BaseLibrary.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositores.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IUserAccount userAccountInterface) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> CreateAsync(Register user)
        {
            if(user == null) return BadRequest("model is Empty");
            var result = await userAccountInterface.CreateAsync(user);
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> SignInAsync(Login user)
        {
            if(user == null) return BadRequest("model is empty");
            var result = await userAccountInterface.SigninAsync(user);
            return Ok(result);
        }
    }
}
