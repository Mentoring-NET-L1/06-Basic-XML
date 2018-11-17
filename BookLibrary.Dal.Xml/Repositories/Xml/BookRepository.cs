using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using BookLibrary.Dal.Constants;
using BookLibrary.Dal.Exceptions;
using BookLibrary.Dal.Models;
using BookLibrary.Dal.ModelVisitors;

namespace BookLibrary.Dal.Repositories.Xml
{
    public class BookRepository : IBookRepository
    {
        public IEnumerable<LibraryItem> Read(Stream xmlStream)
        {
            if (xmlStream == null)
                throw new ArgumentNullException(nameof(xmlStream));

            var settings = new XmlReaderSettings
            {
                IgnoreComments = true,
                IgnoreProcessingInstructions = true,
                IgnoreWhitespace = true,
                ValidationType = ValidationType.Schema,
            };
            var schemaStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(BooksXml.SchemaResourcePath);
            settings.Schemas.Add(XmlSchema.Read(schemaStream, null));

            var reader = XmlReader.Create(xmlStream, settings);
            return new XmlBookRepositoryEnumerator(reader);
        }

        public void Write(Stream xmlStream, IEnumerable<LibraryItem> libraryItems, string libraryName)
        {
            if (xmlStream == null)
                throw new ArgumentNullException(nameof(xmlStream));
            if (libraryItems == null)
                throw new ArgumentNullException(nameof(libraryItems));
            if (libraryName == null)
                throw new ArgumentNullException(nameof(libraryName));

            var writer = XmlWriter.Create(xmlStream, new XmlWriterSettings { Indent = true });
            
            writer.WriteStartElement(BooksXml.RootName, BooksXml.Namespace);
            writer.WriteAttributeString("library", libraryName);
            writer.WriteAttributeString("modifiedDate", XmlConvert.ToString(DateTime.Now));

            var xmlWriteVisitor = new XmlWriteLibraryItemVisitor();
            foreach (var libraryItem in libraryItems)
            {
                libraryItem.Accept(xmlWriteVisitor);
                xmlWriteVisitor.Result.WriteTo(writer);
            }

            writer.WriteEndElement();
            writer.Flush();
        }

        private struct XmlBookRepositoryEnumerator : IEnumerable<LibraryItem>, IEnumerator<LibraryItem>
        {
            private readonly XmlReader _reader;

            public XmlBookRepositoryEnumerator(XmlReader reader)
            {
                Current = null;

                _reader = reader;
                _reader.ReadToFollowing(BooksXml.RootName);
                _reader.ReadStartElement();
            }

            public LibraryItem Current { get; private set; }

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                try
                {
                    if (_reader.Name == BooksXml.RootName) return false;

                    var libraryItemElement = (XElement)XNode.ReadFrom(_reader);
                    Current = XmlLibraryItemFactory.Create(libraryItemElement);
                    return true;
                }
                catch (Exception ex)
                {
                    throw new InvalidDataSourceException("Data source has invalid format.", ex);
                }
            }

            public void Reset()
            {
            }

            public IEnumerator<LibraryItem> GetEnumerator()
            {
                return this;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public void Dispose()
            {
            }
        }
    }
}
