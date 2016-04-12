using System;

namespace services
{
    public class LibraryException : Exception
    {
        public LibraryException():base() { }

        public LibraryException(String msg) : base(msg) { }

        public LibraryException(String msg, Exception ex) : base(msg, ex) { }

    }
}
