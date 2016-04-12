using System;
using System.Collections.Generic;
using chat.model;
using model;

namespace services
{
    public interface ILibraryServer
    {
        User login(String userName, String password, ILibraryClient client);
        void logout(int userId, ILibraryClient client);
        List<Book> getAvailableBooks();
        List<Book> getUserBooks(int userId);
        List<Book> searchBooks(String key);
        void borrowBook(int userId, int bookId);
        void returnBook(int userId, int bookId);
    }
}
