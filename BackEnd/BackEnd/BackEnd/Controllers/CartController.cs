using System.Collections.Generic;
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

            var waitlist = await _context.Waitlists.FirstAsync(x => x.ISBN == copy.ISBN);
            var peopleWaiting = _context.WaitingPeople.Where(x => x.WaitlistListID == waitlist.ListID).ToList();
            
            if (peopleWaiting.Count > 0)
                return BadRequest();
            
            copy.IsAvailable = false;
            _context.BookCopies.Update(copy);
            var userEmail = HttpContext.Items["Email"] as string;
            var user = await _context.Users.FirstAsync(x => x.Email == userEmail);
            var userId = user.UserId ?? default(int);
            await _context.Carts.AddAsync(new Cart(userId, copy.CopyID ?? default(int)));
            await _context.SaveChangesAsync();
            return Ok();
        }
        
        [AuthorizeUser]
        [HttpGet("get")]
        public async Task<List<CartItemResponse>> GetAllCart()
        {

            var userEmail = HttpContext.Items["Email"] as string;
            var user = await _context.Users.FirstAsync(x => x.Email == userEmail);
            var userId = user.UserId ?? default(int);

            var _addedThings = await _context.Carts.Where(x => x.UserId == userId).ToListAsync();
            var result = new List<CartItemResponse>();

            foreach (var v in _addedThings)
            {
                var bc = await _context.BookCopies.FirstAsync(x => x.CopyID == v.BookCopyId);
                var book = GetByISBN(bc.ISBN);

                result.Add(new CartItemResponse {CopyId = bc.CopyID ?? default(int), BookName = book.Name});
            }
            return result;
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

        [AuthorizeUser]
        [HttpGet("towaitlist")]
        public async Task<IActionResult> AddToWaitlist(string ISBN)
        {
            //check if waitlist exists; if not: create a new one for the associated book

            var bookWaitlist = GetWaitlist(ISBN);
            if (bookWaitlist == null)
            {
                var new_waitlist = new Waitlist(ISBN);
                _context.Waitlists.Add(new_waitlist);
                _context.SaveChanges();
            }
            bookWaitlist = GetWaitlist(ISBN);

            var userEmail = HttpContext.Items["Email"] as string;
            var userId = _context.Users.FirstAsync(x => x.Email == userEmail).Id;
            var checkExists =
                _context.WaitingPeople.FirstOrDefault(
                    x => x.UserUserID == userId && x.WaitlistListID == bookWaitlist.ListID);
            if (checkExists == null)
            {
                var waitingList = new Waiting(userId, bookWaitlist.ListID ?? default(int));
                _context.WaitingPeople.Add(waitingList);
                _context.SaveChanges();
            }
            

            return Ok();
        }
    }
}