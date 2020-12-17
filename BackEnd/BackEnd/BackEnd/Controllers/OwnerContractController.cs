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

    public class OwnerContractController : Controller
    {
        private readonly DbAppContext _context;
        private readonly IAuthService _auth;

        public OwnerContractController(DbAppContext context, IAuthService auth)
        {
            _context = context;
            _auth = auth;
        }
        
        public ActionResult<Book> GetByID(string id)
        {
            return _context.Books.FirstOrDefault(x=> x.ISBN == id);
        }


        [AuthorizeUser]
        [HttpPost("addBookCopy")]
        public async Task<IActionResult> CreateBookCopy(AddBookCopyRequest request)
        {
            //1- Check if this book is available
            //if yes: just add a new book copy
            //if not: create a new book then add a new book copy in the db
            
            var searchResult = GetByID(request.Isbn);
            if (searchResult == null)
            {
                //book is not available : must add a book first
                var book_new = new Book(request.Isbn, request.Name, request.Author, request.Genre, request.ReleaseDate, request.Publisher);
                _context.Books.Add(book_new);
                _context.SaveChanges();
                
            }
            //state to be changed
            var av = "New";
            var email = HttpContext.Items["Email"] as string;
            //2-search for users that have this email in the db
            var user = await _context.Users.FirstAsync(x => x.Email == email);
            //3- get the user id
            var userId = user.UserId;
            var ownerContract = new OwnerContract(userId ?? default(int), request.branchID, request.EndDate);
            _context.OwnerContracts.Add(ownerContract);
            _context.SaveChanges();
            int id = ownerContract.ContractID ?? default (int);
            var bookCopyNew = new BookCopy(currentState: av , isAvailable: true, ISBN: request.Isbn, shelfID: 0, OWNERcontractId: id);
            _context.BookCopies.Add(bookCopyNew);
            _context.SaveChanges();
            
            

            return Ok();
        }

        
        
        
        
    }
}