using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using chat.model;
using model;
using services;

namespace networking
{
    public class LibraryClientWorker : ILibraryClient
    {
        private ILibraryServer server;
        private TcpClient connection;

        private NetworkStream stream;
        private IFormatter formatter;
        private volatile bool connected;

        public LibraryClientWorker(ILibraryServer server, TcpClient connection)
        {
            this.server = server;
            this.connection = connection;
            try
            {

                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public virtual void run()
        {
            while (connected)
            {
                try
                {
                    object request = formatter.Deserialize(stream);
                    object response = handleRequest((Request)request);
                    if (response != null)
                    {
                        sendResponse((Response)response);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }

                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
            try
            {
                stream.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }
        }

        public void bookUpdated(int bookId, int newQuantity)
        {
            BookQuantityDTO bookQuantityDto = new BookQuantityDTO(bookId, newQuantity);
            sendResponse(new BorrowBookResponse(bookQuantityDto));
        }

        public void bookReturned(int bookId, string author, string title)
        {
            BookDTO bookDto = new BookDTO(bookId, author, title);
            sendResponse(new ReturnBookResponse(bookDto));
        }

        private Response handleRequest(Request request)
        {
            Response response = null;

            if (request is LoginRequest)
            {
                Console.WriteLine("Login request ...");
                LoginRequest loginRequest = (LoginRequest)request;
                UserDTO userDto = loginRequest.UserDto;
                try
                {
                    User user = null;
                    lock (server)
                    {
                        user = server.login(userDto.UserName, userDto.Password, this);
                    }
                    return new UserLoggedInResponse(user);
                }
                catch (LibraryException e)
                {
                    connected = false;
                    return new ErrorResponse(e.Message);
                }
            }

            if (request is LogoutRequest)
            {
                Console.WriteLine("Logout request");
                LogoutRequest logoutRequest = (LogoutRequest)request;
                int userId = logoutRequest.UserId;
                try
                {
                    lock (server)
                    {

                        server.logout(userId, this);
                    }
                    connected = false;
                    return new OkResponse();

                }
                catch (LibraryException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }

            if (request is GetAvailableBooksRequest)
            {
                Console.WriteLine("SendMessageRequest ...");
                try
                {
                    List<Book> availableBooks = null;
                    lock (server)
                    {
                        availableBooks = server.getAvailableBooks();
                    }
                    return new GetAvailableBooksResponse(availableBooks);
                }
                catch (LibraryException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }

            if (request is GetUserBooksRequest)
            {
                Console.WriteLine("GetLoggedFriends Request ...");
                GetUserBooksRequest getUserBooksRequest = (GetUserBooksRequest)request;
                int userId = getUserBooksRequest.UserId;
                try
                {
                    List<Book> userBooks = null;
                    lock (server)
                    {
                        userBooks = server.getUserBooks(userId);
                    }
                    return new GetUserBooksResponse(userBooks);
                }
                catch (LibraryException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }

            if (request is SearchBooksRequest)
            {
                Console.WriteLine("GetLoggedFriends Request ...");
                SearchBooksRequest searchBooksRequest = (SearchBooksRequest)request;
                String searchKey = searchBooksRequest.SearchKey;
                try
                {
                    List<Book> foundBooks = null;
                    lock (server)
                    {
                        foundBooks = server.searchBooks(searchKey);
                    }
                    return new SearchBooksResponse(foundBooks);
                }
                catch (LibraryException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }

            if (request is BorrowBookRequest)
            {
                Console.WriteLine("GetLoggedFriends Request ...");
                BorrowBookRequest borrowBookRequest = (BorrowBookRequest)request;
                UserBookDTO userBookDto = borrowBookRequest.UserBookDto;
                try
                {
                    lock (server)
                    {
                        server.borrowBook(userBookDto.UserId, userBookDto.BookId);
                    }
                    return new OkResponse();
                }
                catch (LibraryException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }

            if (request is ReturnBookRequest)
            {
                Console.WriteLine("GetLoggedFriends Request ...");
                ReturnBookRequest returnBookRequest = (ReturnBookRequest)request;
                UserBookDTO userBookDto = returnBookRequest.UserBookDto;
                try
                {
                    lock (server)
                    {
                        server.returnBook(userBookDto.UserId, userBookDto.BookId);
                    }
                    return new OkResponse();
                }
                catch (LibraryException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }

            return response;
        }

        private void sendResponse(Response response)
        {
            Console.WriteLine("sending response " + response);
            formatter.Serialize(stream, response);
            stream.Flush();
        }
    }

}