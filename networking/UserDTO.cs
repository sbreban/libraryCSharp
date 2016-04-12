using System;

namespace networking
{
    [Serializable]
    public class UserDTO
    {
        private String userName;
        private String password;

        public UserDTO(String userName, String password)
        {
            this.userName = userName;
            this.password = password;
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public String toString()
        {
            return "UserDTO[" + userName + ' ' + password + "]";
        }
    }

}