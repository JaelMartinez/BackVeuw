// Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyBackend.Data;
using MyBackend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLogin login)
        {
            var user = await _context.Usuarios.SingleOrDefaultAsync(u => u.Email == login.Email && u.Password == login.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var token = GenerateJwtToken(user);

            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UsuarioLogin usuarioLogin)
        {
            if (_context.Usuarios.Any(u => u.Email == usuarioLogin.Email))
            {
                return BadRequest("User already exists");
            }

            var newUser = new Usuario
            {
                Email = usuarioLogin.Email,
                Password = usuarioLogin.Password,
                Nombre = usuarioLogin.Email // Puedes ajustar esto si tienes un campo de nombre diferente
            };

            _context.Usuarios.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully");
        }


        private string GenerateJwtToken(Usuario user)
        {
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) // Asegúrate de incluir este claim
    };

            var key = _configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key is not configured");
            var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
