using model;
using System;
using System.Collections.Generic;

namespace persistence.repository.book
{
    public interface BookRepository
    {
        List<Book> getAvailableBooks();
        List<Book> getUserBooks(int userId);
        List<Book> searchBooks(String key);
        int borrowBook(int userId, int bookId);
        Book returnBook(int userId, int bookId);
    }
}
