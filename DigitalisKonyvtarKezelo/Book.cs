using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalisKonyvtarKezelo
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public string Category { get; set; }
        public Book(string title, string author, int publicationYear, string category)
        {
            Title = title;
            Author = author;
            PublicationYear = publicationYear;
            Category = category;
        }
        public override string ToString()
        {
            return $"{Title},{Author},{PublicationYear},{Category}";
        }
    }
}
