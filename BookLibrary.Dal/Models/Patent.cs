using System;
using System.Collections.Generic;
using BookLibrary.Dal.Models.Visitors;

namespace BookLibrary.Dal.Models
{
    public class Patent : LibraryItem
    {
        public Patent()
        {
            Authors = new List<string>();
        }

        public IList<string> Authors { get; set; }

        public string Country { get; set; }

        public string RegistrationNumber { get; set; }

        public DateTime? ApplicationDate { get; set; }

        public DateTime PublicationDate { get; set; }

        public string Notice { get; set; }

        public override void Accept(ILibraryItemVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
