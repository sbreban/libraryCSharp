using System;

namespace services
{
    public interface ILibraryClient
    {
        void bookUpdated(int bookId, int newQuantity);
        void bookReturned(int bookId, String author, String title);
    }
}
