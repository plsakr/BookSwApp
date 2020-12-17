using System.Threading.Tasks;
using BackEnd.Entities;
using BackEnd.Helpers;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController: Controller
    {
        private readonly DbAppContext _context;

        public CartController(DbAppContext context)
        {
            _context = context;
        }


        [AuthorizeUser]
        [HttpGet("add")]
        public async Task<IActionResult> AddToCart(int copyId)
        {
            var copy = await _context.BookCopies.FirstAsync(x => x.CopyID == copyId);
            
            if (!copy.IsAvailable)
                return BadRequest();
            
            copy.IsAvailable = false;
            _context.BookCopies.Update(copy);
            var userEmail = HttpContext.Items["Email"] as string;
            var userId = _context.Users.FirstAsync(x => x.Email == userEmail).Id;
            await _context.Carts.AddAsync(new Cart(userId, copy.CopyID ?? default(int)));
            await _context.SaveChangesAsync();
            return Ok();
        }
        
        [AuthorizeUser]
        [HttpGet("delete")]
        public async Task<IActionResult> DeleteFromCart(int copyId)
        {
            var copy = await _context.BookCopies.FirstAsync(x => x.CopyID == copyId);
            
            if (copy.IsAvailable)
                return BadRequest();
            
            copy.IsAvailable = true;
            _context.BookCopies.Update(copy);
            var userEmail = HttpContext.Items["Email"] as string;
            var userId = _context.Users.FirstAsync(x => x.Email == userEmail).Id;
            var cartItem = await _context.Carts.FirstAsync(x => x.UserId == userId && x.BookCopyId == copy.CopyID);
            _context.Carts.Remove(cartItem);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}