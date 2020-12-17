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
        
        
        [HttpPost("rentBookCopy")]
        public async Task<IActionResult> RentBookCopy(RentBookCopyRequest request)
        {
            //1- Check if this book is available
            //if yes: just add a new book copy
            //if not: create a new book then add a new book copy in the db

                var bookCopyList = new List<BookCopy>(GetById(request.ISBN));
                if (bookCopyList.Count == 0)
                {
                    //no bookcopy is available 

                }
                else
                {
                    var rentalContract = new RentalContract(request.UserID, request.EndDate);
                    _context.RentalContracts.Add(rentalContract);
                    _context.SaveChanges();
                    int id = rentalContract.RentalContractID ?? default (int);
                    
                    var lentBook = new Contains(id, request.CopyID);
                    _context.Contain.Add(lentBook);
                    _context.SaveChanges();
                }

                return Ok();

        }

    }
}