using System;
using persistence.repository.book;
using persistence.repository.book.database;
using persistence.repository.user;
using persistence.repository.user.xml;

namespace persistence
{
    class XmlPersistence: Persistence
    {
        public override UserRepository createUserRepository()
        {
            Console.WriteLine("XML");
            return new UserRepositoryXml();
        }

        public override BookRepository createBookRepository()
        {
            Console.WriteLine("XML");
            return new BookRepositoryDatabase();
        }

    }
}
