using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

using System.Threading.Tasks;
using BackEnd.Entities;
using BackEnd.Helpers;
using BackEnd.Models;
using BackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly DbAppContext _context;
        private readonly IAuthService _auth;

        public UserController(DbAppContext context, IAuthService auth)
        {
            _context = context;
            _auth = auth;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _auth.AuthenticateUser(request.Email, request.Password);
            if (result == null)
                return BadRequest(new {message = "Username or password incorrect!"});
            
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _auth.RegisterUser(request);

            if (result == null)
                return BadRequest(new {message = "User with given email exists!"});
            
            return Ok(result);
        }

        [AuthorizeUser]
        [HttpGet("test")]
        public IActionResult TestAuth()
        {
            return Ok(HttpContext.Items["Email"]);
        }
        
    }
}