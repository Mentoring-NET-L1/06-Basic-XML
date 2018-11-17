using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using BookLibrary.Dal.Constants;
using BookLibrary.Dal.Models;

namespace BookLibrary.Dal
{
    internal static class XmlLibraryItemFactory
    {
        public static LibraryItem Create(XElement xmlElement)
        {
            if (xmlElement == null)
                throw new ArgumentNullException(nameof(xmlElement));

            switch (xmlElement.Name.LocalName)
            {
                case "book":
                    return CreateBook(xmlElement);
                case "newspaper":
                    return CreateNewspaper(xmlElement);
                case "patent":
                    return CreatePatent(xmlElement);
                default:
                    throw new ArgumentException($"Can't deserialize element {xmlElement.Name.LocalName}", nameof(xmlElement));
            }
        }

        private static Book CreateBook(XElement xmlElement)
        {
            XNamespace ns = BooksXml.Namespace;

            var book = new Book()
            {
                Name = xmlElement.Attribute("name")?.Value,
                PublicationPlace = xmlElement.Attribute("publicationPlace")?.Value,
                Publisher = xmlElement.Attribute("publisher")?.Value,
                PublicationYear = GetInt32Attribute(xmlElement, "publicationYear").Value,
                PageCount = GetInt32Attribute(xmlElement, "pageCount"),
                Notice = xmlElement.Attribute("notice")?.Value,
                Isbn = xmlElement.Attribute("isbn")?.Value,
            };

            GetElementCollection(xmlElement, ns + "authors", book.Authors);

            return book;
        }

        private static Newspaper CreateNewspaper(XElement xmlElement)
        {
            var newspaper = new Newspaper()
            {
                Name = xmlElement.Attribute("name")?.Value,
                PublicationPlace = xmlElement.Attribute("publicationPlace")?.Value,
                Publisher = xmlElement.Attribute("publisher")?.Value,
                PublicationDate = GetDateAttribute(xmlElement, "publicationDate").Value,
                PageCount = GetInt32Attribute(xmlElement, "pageCount"),
                Notice = xmlElement.Attribute("notice")?.Value,
                Issn = xmlElement.Attribute("issn")?.Value,
            };

            return newspaper;
        }

        private static Patent CreatePatent(XElement xmlElement)
        {
            XNamespace ns = BooksXml.Namespace;

            var patent = new Patent()
            {
                Name = xmlElement.Attribute("name")?.Value,
                Country = xmlElement.Attribute("country")?.Value,
                RegistrationNumber = xmlElement.Attribute("registrationNumber")?.Value,
                ApplicationDate = GetDateAttribute(xmlElement, "applicationDate"),
                PublicationDate = GetDateAttribute(xmlElement, "publicationDate").Value,
                PageCount = GetInt32Attribute(xmlElement, "pageCount"),
                Notice = xmlElement.Attribute("notice")?.Value,
            };

            GetElementCollection(xmlElement, ns + "authors", patent.Authors);

            return patent;
        }

        private static int? GetInt32Attribute(XElement element, string attributeName)
        {
            var value = element.Attribute(attributeName)?.Value;
            if (!string.IsNullOrWhiteSpace(value))
                return XmlConvert.ToInt32(value);
            return null;
        }

        private static DateTime? GetDateAttribute(XElement element, string attributeName)
        {
            var value = element.Attribute(attributeName)?.Value;
            if (!string.IsNullOrWhiteSpace(value))
                return XmlConvert.ToDateTime(value);
            return null;
        }

        private static void GetElementCollection(XContainer container, XName elementName, ICollection<string> collection)
        {
            var elementCollection = container.Element(elementName);
            if (elementCollection != null)
            {
                foreach (var subElement in elementCollection.Elements())
                {
                    collection.Add(subElement.Value);
                }
            }
        }
    }
}
