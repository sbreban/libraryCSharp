using System;
using persistence.repository.book;
using persistence.repository.book.database;
using persistence.repository.user;
using persistence.repository.user.database;

namespace persistence
{
    class DatabasePersistence : Persistence
    {
        public override UserRepository createUserRepository()
        {
            Console.WriteLine("Database");
            return new UserRepositoryDatabase();
        }

        public override BookRepository createBookRepository()
        {
            Console.WriteLine("Database");
            return new BookRepositoryDatabase();
        }
    }
}
