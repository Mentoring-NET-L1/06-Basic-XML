using System;
using System.Collections.Generic;
using System.IO;
using BookLibrary.Dal.Exceptions;
using BookLibrary.Dal.Models;
using BookLibrary.Dal.Repositories;
using BookLibrary.Dal.Repositories.Xml;

namespace BookLibrary.ConsoleApp
{
    internal class Program
    {
        private static void Main()
        {
            var xmlStream = File.Open(@"Examples\books.xml", FileMode.Open, FileAccess.ReadWrite);
            try
            {
                IBookRepository repository = new BookRepository();
                var libraryItems = new List<LibraryItem>();

                var writer = new ToStringLibraryItemVisitor();
                foreach (var libraryItem in repository.Read(xmlStream))
                {
                    libraryItems.Add(libraryItem);
                    libraryItem.Accept(writer);
                    Console.WriteLine(writer.Result);
                }

                using (var output = File.Create(@"Examples\new.xml"))
                {
                    repository.Write(output, libraryItems, "New Library");
                }
            }
            catch (RepositoryException ex)
            {
                Console.WriteLine($"Repository exception: {ex.Message}");
            }
            catch (Exception)
            {
                Console.WriteLine("Unexpected exception");
            }
            finally
            {
                xmlStream?.Dispose();
            }
            Console.ReadLine();
        }
    }
}
