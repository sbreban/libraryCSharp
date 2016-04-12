using System;
using System.Collections.Generic;
using System.Xml;
using chat.model;

namespace persistence.repository.user.xml
{
    class UserRepositoryXml : UserRepository
    {
        private IDictionary<string, User> usersDictionary;

        public UserRepositoryXml()
        {
            usersDictionary = new Dictionary<string, User>();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("C:\\Users\\Sergiu\\Downloads\\Library\\persistence\\repository\\user\\xml\\users.xml");
            foreach (XmlNode childNode in xmlDocument.DocumentElement.ChildNodes)
            {
                int id = Int32.Parse(childNode.Attributes["id"].Value);
                String name = childNode.Attributes["name"].Value;
                String password = childNode.Attributes["password"].Value;
                User user = new User(id, password, name, "");
                usersDictionary.Add(name, user);
            }
        }

        public User verifyUser(string userName, string password)
        {
            User user = null;
            if (usersDictionary.ContainsKey(userName))
            {
                user = usersDictionary[userName];
            }
            return user;
        }
    }
}
