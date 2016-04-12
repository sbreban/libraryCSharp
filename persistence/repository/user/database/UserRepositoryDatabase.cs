using System;
using System.Data;
using chat.model;
using MySql.Data.MySqlClient;

namespace persistence.repository.user.database
{
    class UserRepositoryDatabase : UserRepository
    {
        private string myConnectionString = "server=127.0.0.1;uid=librarian;pwd=librarian;database=library;";

        public User verifyUser(string userName, string password)
        {
            Console.WriteLine("Jdbc verify user");
            MySqlConnection connection = null;
            MySqlDataReader mySqlDataReader = null;
            User user = null;
            try
            {
                connection = new MySqlConnection(myConnectionString);
                connection.Open();

                MySqlCommand sqlCommand = new MySqlCommand("select * from users where user_name=@userName and password=@password", connection);
                sqlCommand.Parameters.AddWithValue("@userName", userName);
                sqlCommand.Parameters.AddWithValue("@password", password);

                mySqlDataReader = sqlCommand.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    IDataRecord dataRecord = (IDataRecord)mySqlDataReader;
                    user = new User(dataRecord.GetInt32(0), dataRecord.GetString(1), dataRecord.GetString(2),
                        dataRecord.GetString(3));
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error DB " + e);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
                if (mySqlDataReader != null)
                {
                    mySqlDataReader.Close();
                }
            }
            return user;
        }
    }
}
