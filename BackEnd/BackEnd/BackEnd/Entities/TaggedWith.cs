using System.Collections.Generic;
using Newtonsoft.Json;

namespace BackEnd.Entities
{
    public class TaggedWith
    {
        public string TagName { get; set; }
        public string BookISBN { get; set; }
        
        [JsonIgnore]
        public Book Book { get; set; }

        public TaggedWith(string bookISBN, string tagName)
        {
            TagName = tagName;
            BookISBN = bookISBN;
        }
    }
}