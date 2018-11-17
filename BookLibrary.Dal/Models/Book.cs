using System;
using System.Collections.Generic;
using BookLibrary.Dal.Models.Visitors;

namespace BookLibrary.Dal.Models
{
    public class Book : LibraryItem
    {
        public Book()
        {
            Authors = new List<string>();
        }

        public IList<string> Authors { get; set; }

        public string PublicationPlace { get; set; }

        public string Publisher { get; set; }

        public int PublicationYear { get; set; }

        public string Notice { get; set; }

        public string Isbn { get; set; }

        public override void Accept(ILibraryItemVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
