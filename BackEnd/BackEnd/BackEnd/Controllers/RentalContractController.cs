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

    public class RentalContractController : Controller
    {
        private readonly DbAppContext _context;
        private readonly IAuthService _auth;

        public RentalContractController(DbAppContext context, IAuthService auth)
        {
            _context = context;
            _auth = auth;
        }
        
        [HttpGet("{id}")]
        public List<BookCopy> GetById(string id)
        {
            return _context.BookCopies.Where(x=> x.ISBN == id && x.IsAvailable == true ).ToList();
        }
        
        public Book GetByISBN(string id)
        {
            return _context.Books.FirstOrDefault(x=> x.ISBN == id  );
        }
        
        [AuthorizeUser]
        [HttpGet("checkout")]
        public async Task<IActionResult> RentBookCopy()
        {
            //1- Check if this book is available
            //if yes: just add a new book copy
            //if not:  create a new book then add a new book copy in the db

            var userEmail = HttpContext.Items["Email"] as string;
            var user = await _context.Users.FirstAsync(x => x.Email == userEmail);
            var userId = user.UserId ?? default(int);

            var cartItems = await _context.Carts.Where(x => x.UserId == userId).ToListAsync();

            foreach (var cartItem in cartItems)
            {
                var rentalContract = new RentalContract(userId, DateTime.Now.AddDays(21));
                _context.RentalContracts.Add(rentalContract);
                _context.SaveChanges();
                int id = rentalContract.ContractID ?? default (int);
            
                var lentBook = new Contains(id, cartItem.BookCopyId);
                _context.Contain.Add(lentBook);
                _context.SaveChanges();
            }

            foreach (var cartItem in cartItems)
            {
                _context.Carts.Remove(cartItem);
                _context.SaveChanges();
            }
        

            return Ok();

        }

    }
}