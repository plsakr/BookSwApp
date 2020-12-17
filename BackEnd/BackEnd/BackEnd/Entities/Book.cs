using System;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BackEnd.Entities
{
    public class Book
    {
        public string ISBN { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Publisher { get; set; }

    

    public Book(string Isbn, string name, string author, string genre, DateTime releaseDate, string publisher)
    {
        ISBN = Isbn;
        Name = name;
        Author = author;
        Genre = genre;
        ReleaseDate = releaseDate;
        Publisher = publisher;
    } }

}