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
    public class LibraryServerObjectProxy : ILibraryServer
    {
        private string host;
        private int port;

        private ILibraryClient client;

        private NetworkStream stream;

        private IFormatter formatter;
        private TcpClient connection;

        private Queue<Response> responses;
        private volatile bool finished;
        private EventWaitHandle _waitHandle;
        public LibraryServerObjectProxy(string host, int port)
        {
            this.host = host;
            this.port = port;
            responses = new Queue<Response>();
        }

        public User login(string userName, string password, ILibraryClient client)
        {
            initializeConnection();
            User user = null;
            UserDTO userDto = new UserDTO(userName, password);
            sendRequest(new LoginRequest(userDto));
            Response response = readResponse();
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                closeConnection();
                throw new LibraryException(err.Message);
            }
            if (response is UserLoggedInResponse)
            {
                UserLoggedInResponse userLoggedInResponse = (UserLoggedInResponse)response;
                user = userLoggedInResponse.User;
                this.client = client;
            }
            return user;
        }

        public void logout(int userId, ILibraryClient client)
        {
            sendRequest(new LogoutRequest(userId));
            Response response = readResponse();
            closeConnection();
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                throw new LibraryException(err.Message);
            }
        }

        public List<Book> getAvailableBooks()
        {
            sendRequest(new GetAvailableBooksRequest());
            Response response = readResponse();
            List<Book> availableBooks = null;
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                throw new LibraryException(err.Message);
            }
            if (response is GetAvailableBooksResponse)
            {
                GetAvailableBooksResponse getAvailableBooksResponse = (GetAvailableBooksResponse)response;
                availableBooks = getAvailableBooksResponse.AvailableBooks;
            }
            return availableBooks;
        }

        public List<Book> getUserBooks(int userId)
        {
            sendRequest(new GetUserBooksRequest(userId));
            Response response = readResponse();
            List<Book> userBooks = null;
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                throw new LibraryException(err.Message);
            }
            if (response is GetUserBooksResponse)
            {
                GetUserBooksResponse getUserBooksResponse = (GetUserBooksResponse)response;
                userBooks = getUserBooksResponse.UserBooks;
            }
            return userBooks;
        }

        public List<Book> searchBooks(string key)
        {
            sendRequest(new SearchBooksRequest(key));
            Response response = readResponse();
            List<Book> foundBooks = null;
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                throw new LibraryException(err.Message);
            }
            if (response is SearchBooksResponse)
            {
                SearchBooksResponse searchBooksResponse = (SearchBooksResponse)response;
                foundBooks = searchBooksResponse.FoundBooks;
            }
            return foundBooks;
        }

        public void borrowBook(int userId, int bookId)
        {
            UserBookDTO userBookDto = new UserBookDTO(userId, bookId);
            sendRequest(new BorrowBookRequest(userBookDto));
            Response response = readResponse();
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                throw new LibraryException(err.Message);
            }
        }

        public void returnBook(int userId, int bookId)
        {
            UserBookDTO userBookDto = new UserBookDTO(userId, bookId);
            sendRequest(new ReturnBookRequest(userBookDto));
            Response response = readResponse();
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                throw new LibraryException(err.Message);
            }
        }

        private void closeConnection()
        {
            finished = true;
            try
            {
                stream.Close();
                //output.close();
                connection.Close();
                _waitHandle.Close();
                client = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void sendRequest(Request request)
        {
            try
            {
                formatter.Serialize(stream, request);
                stream.Flush();
            }
            catch (Exception e)
            {
                throw new LibraryException("Error sending object " + e);
            }

        }

        private Response readResponse()
        {
            Response response = null;
            try
            {
                _waitHandle.WaitOne();
                lock (responses)
                {
                    //Monitor.Wait(responses); 
                    response = responses.Dequeue();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return response;
        }

        private void initializeConnection()
        {
            try
            {
                connection = new TcpClient(host, port);
                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                finished = false;
                _waitHandle = new AutoResetEvent(false);
                startReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void startReader()
        {
            Thread tw = new Thread(run);
            tw.Start();
        }

        private void handleUpdate(Response response)
        {
            if (response is BorrowBookResponse)
            {
                BorrowBookResponse borrowBookResponse = (BorrowBookResponse)response;
                BookQuantityDTO bookQuantityDto = borrowBookResponse.BookQuantityDto;
                client.bookUpdated(bookQuantityDto.BookId, bookQuantityDto.NewQuantity);
            }
            if (response is ReturnBookResponse)
            {
                ReturnBookResponse returnBookResponse = (ReturnBookResponse)response;
                BookDTO bookDto = returnBookResponse.BookDto;
                client.bookReturned(bookDto.Id, bookDto.Author, bookDto.Title);
            }
        }

        public virtual void run()
        {
            while (!finished)
            {
                try
                {
                    object response = formatter.Deserialize(stream);
                    Console.WriteLine("response received " + response);
                    if (response is UpdateResponse)
                    {
                        handleUpdate((UpdateResponse)response);
                    }
                    else
                    {
                        lock (responses)
                        {
                            responses.Enqueue((Response)response);
                        }
                        _waitHandle.Set();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Reading error " + e);
                }

            }
        }
    }

}