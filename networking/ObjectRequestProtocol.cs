using System;

namespace networking
{
	public interface Request 
	{
	}


	[Serializable]
	public class LoginRequest : Request
	{
		private UserDTO userDto;

		public LoginRequest(UserDTO userDto)
		{
			this.userDto = userDto;
		}

	    public UserDTO UserDto
	    {
	        get { return userDto; }
	        set { userDto = value; }
	    }
	}

	[Serializable]
	public class LogoutRequest : Request
	{
		private int userId;

		public LogoutRequest(int userId)
		{
			this.userId = userId;
		}

	    public int UserId
	    {
	        get { return userId; }
	        set { userId = value; }
	    }
	}

    [Serializable]
    public class GetAvailableBooksRequest : Request
    {
    }

    [Serializable]
    public class GetUserBooksRequest : Request
    {
        private int userId;

        public GetUserBooksRequest(int userId)
        {
            this.userId = userId;
        }

        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
    }

    [Serializable]
    public class SearchBooksRequest : Request
    {
        private String searchKey;

        public SearchBooksRequest(string searchKey)
        {
            this.searchKey = searchKey;
        }

        public string SearchKey
        {
            get { return searchKey; }
            set { searchKey = value; }
        }
    }

    [Serializable]
    public class BorrowBookRequest : Request
    {
        private UserBookDTO userBookDto;

        public BorrowBookRequest(UserBookDTO userBookDto)
        {
            this.userBookDto = userBookDto;
        }

        public UserBookDTO UserBookDto
        {
            get { return userBookDto; }
            set { userBookDto = value; }
        }
    }

    [Serializable]
    public class ReturnBookRequest : Request
    {
        private UserBookDTO userBookDto;

        public ReturnBookRequest(UserBookDTO userBookDto)
        {
            this.userBookDto = userBookDto;
        }

        public UserBookDTO UserBookDto
        {
            get { return userBookDto; }
            set { userBookDto = value; }
        }
    }


}