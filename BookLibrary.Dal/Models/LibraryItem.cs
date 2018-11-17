using BookLibrary.Dal.Models.Visitors;

namespace BookLibrary.Dal.Models
{
    public abstract class LibraryItem
    {
        public string Name { get; set; }

        public int? PageCount { get; set; }

        public abstract void Accept(ILibraryItemVisitor visitor);
    }
}
