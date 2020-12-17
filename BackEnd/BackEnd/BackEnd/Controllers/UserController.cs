﻿using System;
using System.Collections.Generic;
using System.Linq;
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
using Org.BouncyCastle.Bcpg;
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
        public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
        {
            var result = await _auth.AuthenticateUser(request.Email, request.Password);
            if (result.token == null)
                return BadRequest(new {message = "Username or password incorrect!"});
            
            var option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(1);
            option.HttpOnly = true;
            option.SameSite = SameSiteMode.Lax;
            Response.Cookies.Append("session", result.token, option);
            
            return new AuthResponse {Id = result.userId.GetValueOrDefault(), Role = "USER"};
        }
        
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            var option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(1);
            option.HttpOnly = true;
            option.SameSite = SameSiteMode.Lax;
            Response.Cookies.Append("session", "", option);

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
        public async Task<ActionResult<AuthResponse>> TestAuth()
        {
            var email = HttpContext.Items["Email"] as string;
            var role = HttpContext.Items["Role"] as string;

            if (role == "USER")
            {
                var user = await _context.Users.FirstAsync(x => x.Email == email);
                return new AuthResponse {Id = user.UserId.GetValueOrDefault(), Role = "USER"};
            }
            
            return Ok(HttpContext.Items["Email"]);
        }
        [HttpGet("transactionHistory")]
        public async Task<List<RentalContract>> userTransactionHistory()
        {
            //1-get user email:
            var email = HttpContext.Items["Email"] as string;
            //2-search for users that have this email in the db
            var user = await _context.Users.FirstAsync(x => x.Email == email);
            //3- get the user id
            var userId = user.UserId;
            //4- search for the list of rental contracts saved with this id
            var rentals = _context.RentalContracts.Include(x => x.UserID == userId).ToList();
            return rentals;






        }
        
    }
}