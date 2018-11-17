namespace BookLibrary.Dal.Models.Visitors
{
    public interface ILibraryItemVisitor
    {
        void Visit(Book book);

        void Visit(Newspaper newspaper);

        void Visit(Patent patent);
    }
}
