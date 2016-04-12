using System;
using System.Collections.Generic;
using chat.model;
using model;
using persistence;
using persistence.repository.book;
using persistence.repository.user;
using services;

namespace server
{
    public class LibraryServerImpl : ILibraryServer
    {
        private UserRepository userRepository;
        private BookRepository bookRepository;
        private IDictionary<int, ILibraryClient> loggedClients;

        public LibraryServerImpl()
        {
            userRepository = Persistence.getInstance().createUserRepository();
            bookRepository = Persistence.getInstance().createBookRepository();
            loggedClients = new Dictionary<int, ILibraryClient>();
        }

        public User login(String userName, String password, ILibraryClient client)
        {
            User user = userRepository.verifyUser(userName, password);
            if (user != null)
            {
                if (loggedClients.ContainsKey(user.Id))
                    throw new LibraryException("User already logged in.");
                loggedClients.Add(user.Id, client);
            }
            else
                throw new LibraryException("Authentication failed.");
            return user;
        }

        public void logout(int userId, ILibraryClient client)
        {
            ILibraryClient localClient = loggedClients[userId];
            loggedClients.Remove(userId);
            if (localClient == null)
                throw new LibraryException("User " + userId + " is not logged in.");
        }

        public List<Book> getAvailableBooks()
        {
            return bookRepository.getAvailableBooks();
        }

        public List<Book> getUserBooks(int userId)
        {
            return bookRepository.getUserBooks(userId);
        }

        public List<Book> searchBooks(String key)
        {
            return bookRepository.searchBooks(key);
        }

        public void borrowBook(int userId, int bookId)
        {
            int newQuantity = bookRepository.borrowBook(userId, bookId);
            foreach (KeyValuePair<int, ILibraryClient> pair in loggedClients)
            {
                pair.Value.bookUpdated(bookId, newQuantity);
            }
        }

        public void returnBook(int userId, int bookId)
        {
            Book returned = bookRepository.returnBook(userId, bookId);
            foreach (KeyValuePair<int, ILibraryClient> pair in loggedClients)
            {
                pair.Value.bookReturned(returned.Id, returned.Author, returned.Title);
            }
        }
    }
}
