using System;
using System.Runtime.Serialization;

namespace BookLibrary.Dal.Exceptions
{
    [Serializable]
    public class InvalidDataSourceException : RepositoryException
    {
    public InvalidDataSourceException()
    {
    }

    public InvalidDataSourceException(string message) : base(message)
    {
    }

    public InvalidDataSourceException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected InvalidDataSourceException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
    }
}
