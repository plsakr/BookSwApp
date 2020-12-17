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
    // [EnableCors("MyPolicy")]
    [ApiController]
    [Route("[controller]")]

    public class BookController : Controller
    {
        private readonly DbAppContext _context;
        private readonly IAuthService _auth;

        public BookController(DbAppContext context, IAuthService auth)
        {
            _context = context;
            _auth = auth;
        }


        [HttpPost("addBook")]
        public async Task<IActionResult> AddBook(AddBookRequest request)
        {
            //ADD LONG BLOB FOR IMAGESSSSSSS
            // DON'T FORGET THAT we need to add long blobs 
            var book_new = new Book(request.Isbn, request.Name, request.Author, request.Genre, request.ReleaseDate, request.Publisher);
            _context.Books.Add(book_new);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("byId")]
        public ActionResult<Book> GetById(string id)
        {
            return _context.Books.Include(x => x.Tags).FirstOrDefault(x=> x.ISBN == id);
        }
        
        [HttpGet("")]
        public List<Book> GetAll()
        {
            var books = _context.Books.Include(x => x.Tags).ToList();
            return _context.Books.ToList();
        }
        
        
        
    }

    
}