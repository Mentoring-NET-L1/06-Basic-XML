using System;
using System.Globalization;
using System.Text;
using BookLibrary.Dal.Models;
using BookLibrary.Dal.Models.Visitors;

namespace BookLibrary.ConsoleApp
{
    internal class ToStringLibraryItemVisitor : ILibraryItemVisitor
    {
        private const string WritePropertyFormat = "{0}: {1}\n";

        public string Result { get; private set; }

        public void Visit(Book book)
        {
            DumpLibraryItem((builder) =>
            {
                builder.AppendFormat(WritePropertyFormat, "Name", book.Name);
                builder.AppendFormat(WritePropertyFormat, "Authors", string.Join("; ", book.Authors));
                builder.AppendFormat(WritePropertyFormat, "Publication place", book.PublicationPlace);
                builder.AppendFormat(WritePropertyFormat, "Publisher", book.Publisher);
                builder.AppendFormat(WritePropertyFormat, "Publication year", book.PublicationYear.ToString(CultureInfo.InvariantCulture));
                builder.AppendFormat(WritePropertyFormat, "Notice", book.Notice);
                builder.AppendFormat(WritePropertyFormat, "Isbn", book.Isbn);
                builder.AppendFormat(WritePropertyFormat, "Page count", NulabelIntToString(book.PageCount, CultureInfo.InvariantCulture));
            });
        }

        public void Visit(Newspaper newspaper)
        {
            DumpLibraryItem((builder) =>
            {
                builder.AppendFormat(WritePropertyFormat, "Name", newspaper.Name);
                builder.AppendFormat(WritePropertyFormat, "Publication place", newspaper.PublicationPlace);
                builder.AppendFormat(WritePropertyFormat, "Publisher", newspaper.Publisher);
                builder.AppendFormat(WritePropertyFormat, "Publication date", newspaper.PublicationDate.ToString(CultureInfo.InvariantCulture));
                builder.AppendFormat(WritePropertyFormat, "Notice", newspaper.Notice);
                builder.AppendFormat(WritePropertyFormat, "Issn", newspaper.Issn);
                builder.AppendFormat(WritePropertyFormat, "Page count", NulabelIntToString(newspaper.PageCount, CultureInfo.InvariantCulture));
            });
        }

        public void Visit(Patent patent)
        {
            DumpLibraryItem((builder) =>
            {
                builder.AppendFormat(WritePropertyFormat, "Name", patent.Name);
                builder.AppendFormat(WritePropertyFormat, "Authors", string.Join("; ", patent.Authors));
                builder.AppendFormat(WritePropertyFormat, "Country", patent.Country);
                builder.AppendFormat(WritePropertyFormat, "Application date", NulabelDateToString(patent.ApplicationDate, CultureInfo.InvariantCulture));
                builder.AppendFormat(WritePropertyFormat, "Publication date", patent.PublicationDate.ToString(CultureInfo.InvariantCulture));
                builder.AppendFormat(WritePropertyFormat, "Notice", patent.Notice);
                builder.AppendFormat(WritePropertyFormat, "Registration number", patent.RegistrationNumber);
                builder.AppendFormat(WritePropertyFormat, "Page count", NulabelIntToString(patent.PageCount, CultureInfo.InvariantCulture));
            });
        }

        private void DumpLibraryItem(Action<StringBuilder> wrietAction)
        {
            var builder = new StringBuilder();
            wrietAction(builder);
            Result = builder.ToString();
        }

        private static string NulabelIntToString(int? value, CultureInfo cultureInfo)
        {
            return value?.ToString(cultureInfo) ?? string.Empty;
        }

        private static string NulabelDateToString(DateTime? value, CultureInfo cultureInfo)
        {
            return value?.ToString(cultureInfo) ?? string.Empty;
        }
    }
}
