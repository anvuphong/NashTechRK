using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using MidAssignment.DTO;
using Microsoft.IdentityModel.Tokens;
using MidAssignment.Common;
using MidAssignment.Interfaces;
using MidAssignment.Entities;
using AutoMapper;

namespace MidAssignment.Controllers
{
    [ApiController]
    [Route("api/")]
    public class UserController : ControllerBase
    {
        private string GenerateJwtToken(UserAuthenticationDTO userDTO)
        {
            //generate token that is valid for 7 day
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Constants.SIGNATURE_KEY);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userDTO.UserName ?? "UNKNOWN"),
                new Claim(ClaimTypes.Role, "Administrator")
            };
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.
                HmacSha256Signature)
            };
            var token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }

        private readonly IUserServices _userService;
        private readonly IMapper _mapper;

        public UserController(IUserServices userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(UserAuthenticationDTO userDTO)
        {
            var user = _userService.Authenticate(userDTO);
            if (string.IsNullOrWhiteSpace(userDTO.UserName) ||
            string.IsNullOrWhiteSpace(userDTO.Password))
            {
                if (user.UserId == null) return BadRequest(new { message = "Username or Password is not correct!" });
            }

            return Ok(new
            {
                User = user,
                Token = GenerateJwtToken(userDTO)
            });
        }
    }
}