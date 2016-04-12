using System;

namespace chat.model
{
    [Serializable]
    public class User
    {
        private int id;
        private String userName, password, fullName;

        public User(int id, String password, String userName, String fullName)
        {
            this.id = id;
            this.userName = userName;
            this.password = password;
            this.fullName = fullName;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
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

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }
    }
}
