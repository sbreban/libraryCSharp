using System;
using System.Collections.Generic;
using chat.model;
using model;
using networking;
using services;

namespace client
{

    public class LibraryClientController : ILibraryClient
    {
        public event EventHandler<LibraryUserEventArgs> updateEvent; //ctrl calls it when it has received an update
        private readonly ILibraryServer server;
        private User currentUser;

        private IList<Book> availableBooks;
        private IList<Book> userBooks;

        public LibraryClientController(ILibraryServer server)
        {
            this.server = server;
            currentUser = null;
        }

        public void login(String userName, String password)
        {
            User user = server.login(userName, password, this);
            this.currentUser = user;

            Console.WriteLine("Login succeeded ....");
            Console.WriteLine("Current user {0}", user);
        }

        public void logout()
        {
            Console.WriteLine("Ctrl logout");
            server.logout(currentUser.Id, this);
            currentUser = null;
        }

        public void borrowBook(int bookId)
        {
            server.borrowBook(currentUser.Id, bookId);
        }

        public void returnBook(int bookId)
        {
            server.returnBook(currentUser.Id, bookId);
        }

        public void returnBook(int userId, int bookId)
        {
            server.returnBook(userId, bookId);
        }

        public List<Book> searchBooks(String searchKey)
        {
            return server.searchBooks(searchKey);
        }

        public void bookUpdated(int bookId, int newQuantity)
        {
            BookQuantityDTO bookQuantityDto = new BookQuantityDTO(bookId, newQuantity);
            LibraryUserEventArgs libraryUserEventArgs = new LibraryUserEventArgs(LibraryUserEvent.BookUpdated, bookQuantityDto);
            Console.WriteLine("Book updated");
            OnUserEvent(libraryUserEventArgs);
        }

        public void bookReturned(int bookId, string author, string title)
        {
            BookDTO bookDto = new BookDTO(bookId, author, title);
            LibraryUserEventArgs libraryUserEventArgs = new LibraryUserEventArgs(LibraryUserEvent.BookReturned, bookDto);
            Console.WriteLine("Book returned");
            OnUserEvent(libraryUserEventArgs);
        }

        public IList<Book> getAvailableBooks()
        {
            availableBooks = server.getAvailableBooks();
            return availableBooks;
        }

        public IList<Book> getUserBooks()
        {
            userBooks = server.getUserBooks(currentUser.Id);
            return userBooks;
        }

        protected virtual void OnUserEvent(LibraryUserEventArgs e)
        {
            if (updateEvent == null)
                return;
            updateEvent(this, e);
            Console.WriteLine("Update Event called");
        }
    }
}
