using System;
using System.Reflection;
using persistence.repository.book;
using persistence.repository.user;

namespace persistence
{
    public abstract class Persistence
    {
        private static Persistence instance = null;

        public abstract UserRepository createUserRepository();

        public abstract BookRepository createBookRepository();

        public static Persistence getInstance()
        {
            if (instance == null)
            {
                Assembly assem = Assembly.GetExecutingAssembly();
                Type[] types = assem.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsSubclassOf(typeof(Persistence)) && type.Name.Equals("XmlPersistence"))
                        instance = (Persistence)Activator.CreateInstance(type);
                }
            }
            return instance;
        }
    }
}
