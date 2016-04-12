using System;
using System.Collections.Generic;
using chat.model;
using model;

namespace networking
{
	public interface Response 
	{
	}

    public interface UpdateResponse : Response
    {
        
    }

	[Serializable]
	public class OkResponse : Response
	{		
	}

    [Serializable]
	public class ErrorResponse : Response
	{
		private string message;

		public ErrorResponse(string message)
		{
			this.message = message;
		}

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
	}

    [Serializable]
    public class UserLoggedInResponse : Response
    {
        private User user;

        public UserLoggedInResponse(User user)
        {
            this.user = user;
        }

        public User User
        {
            get { return user; }
            set { user = value; }
        }
    }

    [Serializable]
    public class GetAvailableBooksResponse : Response
    {
        private List<Book>  availableBooks;

        public GetAvailableBooksResponse(List<Book> availableBooks)
        {
            this.availableBooks = availableBooks;
        }

        public List<Book> AvailableBooks
        {
            get { return availableBooks; }
            set { availableBooks = value; }
        }
    }

    [Serializable]
    public class GetUserBooksResponse : Response
    {
        private List<Book>  userBooks;

        public GetUserBooksResponse(List<Book> userBooks)
        {
            this.userBooks = userBooks;
        }

        public List<Book> UserBooks
        {
            get { return userBooks; }
            set { userBooks = value; }
        }
    }

    [Serializable]
    public class SearchBooksResponse : Response
    {
        private List<Book>  foundBooks;

        public SearchBooksResponse(List<Book> foundBooks)
        {
            this.foundBooks = foundBooks;
        }

        public List<Book> FoundBooks
        {
            get { return foundBooks; }
            set { foundBooks = value; }
        }
    }

    [Serializable]
    public class BorrowBookResponse : UpdateResponse
    {
        BookQuantityDTO bookQuantityDto;

        public BorrowBookResponse(BookQuantityDTO bookQuantityDto)
        {
            this.bookQuantityDto = bookQuantityDto;
        }

        public BookQuantityDTO BookQuantityDto
        {
            get { return bookQuantityDto; }
            set { bookQuantityDto = value; }
        }
    }

    [Serializable]
    public class ReturnBookResponse : UpdateResponse
    {
        private BookDTO bookDto;

        public ReturnBookResponse(BookDTO bookDto)
        {
            this.bookDto = bookDto;
        }

        public BookDTO BookDto
        {
            get { return bookDto; }
            set { bookDto = value; }
        }
    }


}