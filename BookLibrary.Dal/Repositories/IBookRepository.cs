using System.Collections.Generic;
using System.IO;
using BookLibrary.Dal.Models;

namespace BookLibrary.Dal.Repositories
{
    public interface IBookRepository
    {
        IEnumerable<LibraryItem> Read(Stream xmlStream);

        void Write(Stream xmlStream, IEnumerable<LibraryItem> libraryItems, string libraryName);
    }
}
