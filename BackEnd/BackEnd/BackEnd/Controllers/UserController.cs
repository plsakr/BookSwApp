using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BackEnd.Entities;
using BackEnd.Helpers;
using BackEnd.Models;
using BackEnd.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using SameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode;

namespace BackEnd.Controllers
{
    // [EnableCors("MyPolicy")]
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
            
            var option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(1);
            option.HttpOnly = true;
            option.SameSite = SameSiteMode.Lax;
            Response.Cookies.Append("session", result, option);
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _auth.RegisterUser(request);

            if (result == null)
                return BadRequest(new {message = "User with given email exists!"});
            
            var option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(1);
            option.HttpOnly = true;
            Response.Cookies.Append("session", result, option);
            return Ok();
        }

        [AuthorizeUser]
        [HttpGet("test")]
        public IActionResult TestAuth()
        {
            return Ok(HttpContext.Items["Email"]);
        }
        
    }
}