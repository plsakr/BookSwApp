using System.Linq;
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
    public class CartController : Controller
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

        public Book GetByISBN(string id)
        {
            return _context.Books.FirstOrDefault(x => x.ISBN == id);
        }

        public Waitlist GetWaitlist(string id)
        {
            return _context.Waitlists.FirstOrDefault(x => x.ISBN == id);
        }

        [HttpPost("towaitlist")]
        public async Task<IActionResult> AddToWaitlist(WaitOnBookRequest request)
        {
            //check if waitlist exists; if not: create a new one for the associated book

            var bookWaitlist = GetWaitlist(request.ISBN);
            if (bookWaitlist == null)
            {
                var originalBook = GetByISBN(request.ISBN);
                var new_waitlist = new Waitlist(request.ISBN);
                _context.Waitlists.Add(new_waitlist);
                _context.SaveChanges();
            }

            var waitingList = new Waiting(request.UserID, request.waitlistID);
            _context.WaitingPeople.Add(waitingList);
            _context.SaveChanges();

            return Ok();
        }
    }
}