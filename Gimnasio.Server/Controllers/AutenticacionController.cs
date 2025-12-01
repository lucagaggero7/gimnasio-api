using Gimnasio.Server.Servicios.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gimnasio.Server.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AutenticacionController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly JwtService _jwt;

        public AutenticacionController(IConfiguration config, JwtService jwt)
        {
            _config = config;
            _jwt = jwt;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest req)
        {
            var user = _config["AuthUser"];
            var pass = _config["AuthPassword"];

            if (req.Username == user && req.Password == pass)
            {
                var token = _jwt.GenerateToken(req.Username);
                return Ok(new { token });
            }

            return Unauthorized("Credenciales incorrectas");
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

}
