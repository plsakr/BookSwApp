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
            var userId = _context.Users.FirstAsync(x => x.Email == email).Id;

            var isWaiting = _context.WaitingPeople.Where(x => x.UserID == userId).ToList();
            var result = new List<WaitlistStatusReponse>();
            foreach (var waiting in isWaiting)
            {
                var waitlist = _context.Waitlists.First(x => x.WaitlistID == waiting.WaitlistID);
                var bookName = _context.Books.First(x => x.ISBN == waitlist.ISBN).Name;

                var allPeopleWaiting =
                    _context.WaitingPeople.Where(x => x.WaitlistID == waitlist.WaitlistID).ToList();

                var sortedPeopleWaiting = allPeopleWaiting.OrderBy(x => x.EntryTime);
                int pos = 0;
                foreach (var waiting1 in sortedPeopleWaiting)
                {
                    if (waiting1.UserID == userId)
                        break;
                    pos++;
                }
                
                result.Add(new WaitlistStatusReponse {BookName = bookName, Position = pos});
            }

            return result;
        }


        [HttpGet("{id}")]
        public ActionResult<List<BookCopy>> GetById(string id, bool isAv)
        {
            return _context.BookCopies.Where(x=> x.ISBN == id && x.IsAvailable == isAv ).ToList();
        }
        
        
        
        
    }
    
}
