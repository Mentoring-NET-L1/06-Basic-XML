using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using BookLibrary.Dal.Constants;
using BookLibrary.Dal.Models;
using BookLibrary.Dal.Models.Visitors;

namespace BookLibrary.Dal.ModelVisitors
{
    internal class XmlWriteLibraryItemVisitor : ILibraryItemVisitor
    {
        public XElement Result { get; private set; }

        public void Visit(Book book)
        {
            XNamespace ns = BooksXml.Namespace;
            Result = new XElement(ns + "book");

            AddAttribute(Result, "name", book.Name);
            AddAttribute(Result, "publicationPlace", book.PublicationPlace);
            AddAttribute(Result, "publisher", book.Publisher);
            AddAttribute(Result, "publicationYear", XmlConvert.ToString(book.PublicationYear));
            AddAttribute(Result, "pageCount", book.PageCount, XmlConvert.ToString);
            AddAttribute(Result, "notice", book.Notice);
            AddAttribute(Result, "isbn", book.Isbn);

            AddAuthors(Result, book.Authors);
        }

        public void Visit(Newspaper newspaper)
        {
            XNamespace ns = BooksXml.Namespace;
            Result = new XElement(ns + "newspaper");

            AddAttribute(Result, "name", newspaper.Name);
            AddAttribute(Result, "publicationPlace", newspaper.PublicationPlace);
            AddAttribute(Result, "publisher", newspaper.Publisher);
            AddAttribute(Result, "publicationDate", XmlConvert.ToString(newspaper.PublicationDate));
            AddAttribute(Result, "pageCount", newspaper.PageCount, XmlConvert.ToString);
            AddAttribute(Result, "notice", newspaper.Notice);
            AddAttribute(Result, "issn", newspaper.Issn);
        }

        public void Visit(Patent patent)
        {
            XNamespace ns = BooksXml.Namespace;
            Result = new XElement(ns + "patent");

            AddAttribute(Result, "name", patent.Name);
            AddAttribute(Result, "country", patent.Country);
            AddAttribute(Result, "registrationNumber", patent.RegistrationNumber);
            AddAttribute(Result, "applicationDate", patent.ApplicationDate, XmlConvert.ToString);
            AddAttribute(Result, "publicationDate", XmlConvert.ToString(patent.PublicationDate));
            AddAttribute(Result, "pageCount", patent.PageCount, XmlConvert.ToString);
            AddAttribute(Result, "notice", patent.Notice);

            AddAuthors(Result, patent.Authors);
        }

        private static void AddAttribute<T>(XElement element, string name, T? value, Func<T, string> toString) where T: struct
        {
            if (value.HasValue)
                AddAttribute(element, name, toString(value.Value));
        }

        private static void AddAttribute(XElement element, string name, string value)
        {
            if (value != null)
                element.Add(new XAttribute(name, value));
        }

        private static void AddAuthors(XElement element, IList<string> authors)
        {
            XNamespace ns = BooksXml.Namespace;
            if (authors.Count > 0)
            {
                var authorElements = new XElement(ns + "authors");
                foreach (var author in authors)
                {
                    authorElements.Add(new XElement(ns + "author", author));
                }
                element.Add(authorElements);
            }
        }
    }
}
