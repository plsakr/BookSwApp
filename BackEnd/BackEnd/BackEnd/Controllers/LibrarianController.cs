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
        
        public List<TransactionLists> GetByEndDate()
        {
            
            List<RentalContract> listofExpiringRentalContracts = new List<RentalContract>();
            List<TransactionLists> RentalTransactions = new List<TransactionLists>();
            var currentDate = DateTime.Today;
            
            //need to get a list of rental contracts
            var listOfAllRentals = _context.RentalContracts;
            foreach (var rentalUser in listOfAllRentals)
            {
                if (rentalUser.EndDate == currentDate)
                {
                    //bado: list fiya book name DONE start data DONE end date DONE branch name DONE and user name Done
                    listofExpiringRentalContracts.Add(rentalUser);
                    //set the is available to 1
                    var currentRentalID = rentalUser.ContractID ?? default (int);
                    var containsBookCopyID =
                        _context.Contain.FirstOrDefault(x => x.Rental_ContractcontractID == currentRentalID);
                    var currentBookCopyID = containsBookCopyID.Book_CopycopyID;
                    var bookCopy = _context.BookCopies.FirstOrDefault(x => x.CopyID == currentBookCopyID);
                    var bookIsbn = bookCopy.ISBN;
                    var currentBook = _context.Books.FirstOrDefault(x => x.ISBN == bookIsbn);
                    var bookName = currentBook.Name;
                    var startDate = rentalUser.StartDate;
                    var endDate = rentalUser.EndDate;
                    var ownerContractID = bookCopy.OWNERcontractId;
                    var ownerContractCurrent =
                        _context.OwnerContracts.FirstOrDefault(x => x.ContractID == ownerContractID);
                    var branchId = ownerContractCurrent.BranchID;
                    var currentBranch = _context.Branches.FirstOrDefault(x => x.BranchID == branchId);
                    var branchName = currentBranch.Name;
                    var userID = ownerContractCurrent.UserID;
                    var user = _context.Users.FirstOrDefault(x => x.UserId == userID);
                    var userName = user.Name;

                    var tran = new TransactionLists(bookName, startDate, endDate, branchName, userName);
                    RentalTransactions.Add(tran);
                    
                    
                    bookCopy.IsAvailable = true;
                }
            }

            return RentalTransactions;
        }


        public void ClearRentalByEndDate()
        {
            var toclear = GetByEndDate();
            foreach (var i in toclear)
            {
                var bookName = i.BookName;
                var book = _context.Books.FirstOrDefault(x => x.Name == bookName);
                var bookCopyCurrent = _context.BookCopies.FirstOrDefault(x => x.ISBN == book.ISBN);
                bookCopyCurrent.IsAvailable = true;
            }
            
        }



        public List<TransactionLists> GetOwnersByEndDate()
        {
            List<OwnerContract> listofExpiringOwnerContracts = new List<OwnerContract>();
            List<TransactionLists> listOfOwnersToClear = new List<TransactionLists>();
            var currentDate = DateTime.Today;
            
       
            var listOfAllOwners = _context.OwnerContracts;
            foreach (var ownerUser in listOfAllOwners)
            {
                if (ownerUser.EndDate == currentDate)
                {
                    listofExpiringOwnerContracts.Add(ownerUser);

                    var branchId = ownerUser.BranchID;
                    var userID = ownerUser.UserID;
                    var startDate = ownerUser.StartDate;
                    var endDate = ownerUser.EndDate;
                    var ownerContractID = ownerUser.ContractID;

                    var bookCopy = _context.BookCopies.FirstOrDefault(x => x.OWNERcontractId == ownerContractID);
                    var bookISBN = bookCopy.ISBN;
                    var book = _context.Books.FirstOrDefault(x => x.ISBN == bookISBN);
                    var bookName = book.Name;

                    var branch = _context.Branches.FirstOrDefault(x => x.BranchID == branchId);
                    var branchName = branch.Name;

                    var user = _context.Users.FirstOrDefault(x => x.UserId == userID);
                    var userName = user.Name;
                    
                    var transactionOwner = new TransactionLists(bookName, startDate, endDate, branchName, userName);
                    listOfOwnersToClear.Add(transactionOwner);
                    
                }
            }

            return listOfOwnersToClear;
        }
        
        public void ClearOwnerByEndDate()
        {
            var toclear = GetOwnersByEndDate();
            foreach (var i in toclear)
            {
                var bookName = i.BookName;
                var book = _context.Books.FirstOrDefault(x => x.Name == bookName);
                var bookCopyCurrent = _context.BookCopies.FirstOrDefault(x => x.ISBN == book.ISBN);
                bookCopyCurrent.IsAvailable = false;
            }
            
        }

    }
}



    