using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Entities;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace BackEnd.Services
{
    public interface IAuthService
    {
        public Task<string> AuthenticateUser(string email, string simplePass);
        public Task<string> RegisterUser(RegisterRequest req);
    }
    public class AuthService: IAuthService
    {
        private readonly DbAppContext _context;
        private const string SECRET_KEY = "THIS IS USED TO SIGN AND VERIFY JWT TOKENS, IT CAN BE ANY STRING";

        public AuthService(DbAppContext context)
        {
            _context = context;
        }

        public async Task<string> AuthenticateUser(string email, string simplePass)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            var hashedTry = HashPassword(simplePass, user.Salt);
            if (hashedTry != user.Password)
                return null;
            
            var token = generateJwtToken(email);
            return token;
        }

        public async Task<string> RegisterUser(RegisterRequest req)
        {
            var otherUsers = await _context.Users.FirstOrDefaultAsync(x => x.Email == req.Email);
            if (otherUsers != null)
                return null;

            var newSalt = GetSalt();
            var hashedPass = HashPassword(req.Password, newSalt);
            
            var user = new User(req.Name, req.Email, hashedPass, newSalt);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return generateJwtToken(user.Email);
        }
        
        private static int saltLengthLimit = 64;
        private static string GetSalt()
        {
            return Convert.ToBase64String(GetSalt(saltLengthLimit));
        }
        private static byte[] GetSalt(int maximumSaltLength)
        {
            var salt = new byte[maximumSaltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }
        
        
        private string generateJwtToken(string email)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SECRET_KEY);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", email), new Claim("role", "USER") }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static string HashPassword(string plainPassword, string salt)
        {
            byte[] passwordAsBytes = Encoding.UTF8.GetBytes(plainPassword);
            byte[] saltBytes = Convert.FromBase64String(salt);
            List<byte> passwordWithSaltBytes = new List<byte>();
            passwordWithSaltBytes.AddRange(passwordAsBytes);
            passwordWithSaltBytes.AddRange(saltBytes);
            byte[] digestBytes = SHA256.Create().ComputeHash(passwordWithSaltBytes.ToArray());
            return Convert.ToBase64String(digestBytes);

        }
    }
}