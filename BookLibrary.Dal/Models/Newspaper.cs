using System;
using BookLibrary.Dal.Models.Visitors;

namespace BookLibrary.Dal.Models
{
    public class Newspaper : LibraryItem
    {
        public string PublicationPlace { get; set; }

        public string Publisher { get; set; }

        public DateTime PublicationDate { get; set; }

        public string Notice { get; set; }

        public string Issn { get; set; }

        public override void Accept(ILibraryItemVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
