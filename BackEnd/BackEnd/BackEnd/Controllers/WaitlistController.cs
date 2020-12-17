using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
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
    [ApiController]
    [Route("[controller]")]

    public class WaitlistController : Controller
    {
        private readonly DbAppContext _context;
        private readonly IAuthService _auth;

        public WaitlistController(DbAppContext context, IAuthService auth)
        {
            _context = context;
            _auth = auth;
        }

        [AuthorizeUser]
        [HttpGet()]
        public async Task<List<WaitlistStatusReponse>> GetWaitListStatus()
        {
            var email = HttpContext.Items["Email"] as string;
            var user = await _context.Users.FirstAsync(x => x.Email == email);
            var userId = user.UserId;
            var isWaiting = _context.WaitingPeople.Where(x => x.UserUserID == userId).ToList();
            var result = new List<WaitlistStatusReponse>();
            foreach (var waiting in isWaiting)
            {
                var waitlist = _context.Waitlists.First(x => x.ListID == waiting.WaitlistListID);
                var book = _context.Books.First(x => x.ISBN == waitlist.ISBN);
                var bookName = book.Name;

                var allPeopleWaiting =
                    _context.WaitingPeople.Where(x => x.WaitlistListID == waitlist.ListID).ToList();

                var sortedPeopleWaiting = allPeopleWaiting.OrderBy(x => x.EntryDate);
                int pos = 0;
                foreach (var waiting1 in sortedPeopleWaiting)
                {
                    if (waiting1.UserUserID == userId)
                        break;
                    pos++;
                }

                var bookCopies = await _context.BookCopies.Where(x => x.ISBN == book.ISBN && x.IsAvailable == true).ToListAsync();
                if (bookCopies.Count > 0)
                    pos--;
                
                result.Add(new WaitlistStatusReponse {BookName = bookName, Position = pos});
            }

            return result;
        }
        
        [AuthorizeUser]
        [HttpGet("add")]
        public async Task<IActionResult> AddToCart(string isbn)
        {
            var copy = await _context.BookCopies.FirstAsync(x => x.ISBN == isbn && x.IsAvailable == true);

            var userEmail = HttpContext.Items["Email"] as string;
            var user = await _context.Users.FirstAsync(x => x.Email == userEmail);
            var userId = user.UserId ?? default(int);
            
            if (!copy.IsAvailable)
                return BadRequest();

            var waitlist = await _context.Waitlists.FirstAsync(x => x.ISBN == copy.ISBN);
            var meWaiting =
                await _context.WaitingPeople.FirstAsync(x => x.WaitlistListID == waitlist.ListID && x.UserUserID == userId);
            _context.WaitingPeople.Remove(meWaiting);
            copy.IsAvailable = false;
            _context.BookCopies.Update(copy);
            
            await _context.Carts.AddAsync(new Cart(userId, copy.CopyID ?? default(int)));
            await _context.SaveChangesAsync();
            return Ok();
        }


        // [HttpGet("{id}")]
        // public ActionResult<List<BookCopy>> GetById(string id, bool isAv)
        // {
        //     return _context.BookCopies.Where(x=> x.ISBN == id && x.IsAvailable == isAv ).ToList();
        // }
        
        
        
        
    }
    
}
