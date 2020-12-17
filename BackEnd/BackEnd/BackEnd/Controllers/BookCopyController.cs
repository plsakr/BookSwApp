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

    public class BookCopyController : Controller
    {
        private readonly DbAppContext _context;
        private readonly IAuthService _auth;

        public BookCopyController(DbAppContext context, IAuthService auth)
        {
            _context = context;
            _auth = auth;
        }


        [HttpGet("byId")]
        public ActionResult<List<BookCopy>> GetById(string id)
        {
            return _context.BookCopies.Where(x=> x.ISBN == id && x.IsAvailable ).ToList();
        }
        
        
        
        
    }

    
}