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

    public class LibrarianController : Controller
    {
        private readonly DbAppContext _context;
        private readonly IAuthService _auth;

        public LibrarianController(DbAppContext context, IAuthService auth)
        {
            _context = context;
            _auth = auth;
        }
        
        public ActionResult<List<RentalContract>> GetByEndDate()
        {
            List<RentalContract> listofExpiringRentalContracts = new List<RentalContract>();
            var currentDate = DateTime.Today;
            
            //need to get a list of rental contracts
            var listOfAllRentals = _context.RentalContracts;
            foreach (var rentalUser in listOfAllRentals)
            {
                if (rentalUser.EndDate == currentDate)
                {
                    listofExpiringRentalContracts.Add(rentalUser);
                    //set the is available to 1
                    var currentRentalID = rentalUser.ContractID ?? default (int);
                    var containsBookCopyID =
                        _context.Contain.FirstOrDefault(x => x.Rental_ContractcontractID == currentRentalID);
                    var currentBookCopyID = containsBookCopyID.Book_CopycopyID;
                    var bookCopy = _context.BookCopies.FirstOrDefault(x => x.CopyID == currentBookCopyID);
                    bookCopy.IsAvailable = true;
                }
            }

            return listofExpiringRentalContracts;
        }
        
        public ActionResult<List<OwnerContract>> GetOwnersByEndDate()
        {
            List<OwnerContract> listofExpiringOwnerContracts = new List<OwnerContract>();
            var currentDate = DateTime.Today;
            
            //need to get a list of rental contracts
            var listOfAllOwners = _context.OwnerContracts;
            foreach (var ownerUser in listOfAllOwners)
            {
                if (ownerUser.EndDate == currentDate)
                {
                    listofExpiringOwnerContracts.Add(ownerUser);
                    //set the is available to 0
                    
                    var currentBookCopy = _context.BookCopies.FirstOrDefault(x => x.OWNERcontractId == ownerUser.ContractID);
                    currentBookCopy.IsAvailable = false;
                    
                }
            }

            return listofExpiringOwnerContracts;
        }

    }
}

    