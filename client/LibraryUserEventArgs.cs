using System;

namespace client
{
    public enum LibraryUserEvent
    {
        BookUpdated, BookReturned
    }

    public class LibraryUserEventArgs : EventArgs
    {
        private readonly LibraryUserEvent userEvent;
        private readonly Object data;

        public LibraryUserEventArgs(LibraryUserEvent userEvent, object data)
        {
            this.userEvent = userEvent;
            this.data = data;
        }

        public LibraryUserEvent UserEventType
        {
            get { return userEvent; }
        }

        public object Data
        {
            get { return data; }
        }
    }
}
