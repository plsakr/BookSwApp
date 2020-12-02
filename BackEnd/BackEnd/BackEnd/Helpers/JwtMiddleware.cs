using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private const string SECRET = "THIS IS USED TO SIGN AND VERIFY JWT TOKENS, IT CAN BE ANY STRING";

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (context.Request.Cookies.ContainsKey("session"))
            {
                var token = context.Request.Cookies["session"];
                if (token != "")
                    attachUserToContext(context, token);
            }

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(SECRET);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "id").Value;
                var userRole = jwtToken.Claims.First(x => x.Type == "role").Value;

                // attach user to context on successful jwt validation
                context.Items["Email"] = userId;
                context.Items["Role"] = userRole;
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}