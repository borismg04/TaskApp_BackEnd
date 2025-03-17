using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using TaskAppBackEnd.Interface;

namespace TaskAppBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogin _login;

        public LoginController(ILogin login)
        {
            _login = login;
        }

        [HttpGet]
        [Route("login")]

        public IActionResult Login(string email, string pass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _login.Authenticate(email, pass);

            return StatusCode(result.statusCode, result);
        }
    }
}
