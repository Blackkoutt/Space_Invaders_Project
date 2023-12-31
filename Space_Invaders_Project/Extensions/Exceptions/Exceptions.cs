using System;

namespace Space_Invaders_Project.Extensions.Exceptions
{
    public class CannotFindFileException:Exception
    {
        public CannotFindFileException(string message):base(message) { }
    }
    public class UnknownException : Exception
    {
        public UnknownException(string message) : base(message) { }
    }
    public class CannotConvertException : Exception
    {
        public CannotConvertException(string message) : base(message) { }
    }
    public class FileSyntaxException : Exception
    {
        public FileSyntaxException(string message) : base(message) { }
    }
}
