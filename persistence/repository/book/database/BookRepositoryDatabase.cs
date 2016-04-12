using model;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace persistence.repository.book.database
{
    class BookRepositoryDatabase : BookRepository
    {
        private string myConnectionString = "server=127.0.0.1;uid=librarian;pwd=librarian;database=library;";

        public List<Book> getAvailableBooks()
        {
            Console.WriteLine("Load available books");
            MySqlConnection connection = null;
            MySqlDataReader mySqlDataReader = null;
            List<Book> availableBooks = new List<Book>();
            try
            {
                connection = new MySqlConnection(myConnectionString);
                connection.Open();

                MySqlCommand sqlCommand = new MySqlCommand("select * from books where available>0", connection);

                mySqlDataReader = sqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    IDataRecord dataRecord = (IDataRecord)mySqlDataReader;
                    availableBooks.Add(new Book(dataRecord.GetInt32(0), dataRecord.GetString(1), dataRecord.GetString(2), dataRecord.GetInt32(3)));
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
            return availableBooks;
        }

        public List<Book> getUserBooks(int userId)
        {
            Console.WriteLine("Load user's books");
            MySqlConnection connection = null;
            MySqlDataReader mySqlDataReader = null;
            List<Book> usersBooks = new List<Book>();
            try
            {
                connection = new MySqlConnection(myConnectionString);
                connection.Open();

                String sql = "select * from books b " +
                             "inner join user_book ub on ub.book_id=b.id " +
                             "inner join users u on u.id=ub.user_id " +
                             "where u.id=@userId";
                MySqlCommand sqlCommand = new MySqlCommand(sql, connection);
                sqlCommand.Parameters.AddWithValue("@userId", userId);

                mySqlDataReader = sqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    IDataRecord dataRecord = (IDataRecord)mySqlDataReader;
                    usersBooks.Add(new Book(dataRecord.GetInt32(0), dataRecord.GetString(1), dataRecord.GetString(2),
                        dataRecord.GetInt32(3)));
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
            return usersBooks;
        }

        public List<Book> searchBooks(String key)
        {
            Console.WriteLine("Search books");
            MySqlConnection connection = null;
            MySqlDataReader mySqlDataReader = null;
            List<Book> foundBooks = new List<Book>();
            try
            {
                connection = new MySqlConnection(myConnectionString);
                connection.Open();

                MySqlCommand sqlCommand = new MySqlCommand("select * from books where title like @searchKey", connection);
                string searchKey = string.Format("%{0}%", key);
                sqlCommand.Parameters.AddWithValue("@searchKey", searchKey);

                mySqlDataReader = sqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    IDataRecord dataRecord = (IDataRecord)mySqlDataReader;
                    foundBooks.Add(new Book(dataRecord.GetInt32(0), dataRecord.GetString(1), dataRecord.GetString(2),
                        dataRecord.GetInt32(3)));
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
            return foundBooks;
        }

        public int borrowBook(int userId, int bookId)
        {
            Console.WriteLine("Borrow book");
            MySqlConnection connection = null;
            MySqlDataReader mySqlDataReader = null;
            int quantity = 0;
            try
            {
                connection = new MySqlConnection(myConnectionString);
                connection.Open();

                String changeQuantity = "update books set available=available-1 where id=@bookId";
                MySqlCommand changeQuantityCommand = new MySqlCommand(changeQuantity, connection);
                changeQuantityCommand.Parameters.AddWithValue("@bookId", bookId);
                changeQuantityCommand.ExecuteNonQuery();

                String borrow = "insert into user_book values(@userId,@bookId)";
                MySqlCommand borrowCommand = new MySqlCommand(borrow, connection);
                borrowCommand.Parameters.AddWithValue("@userId", userId);
                borrowCommand.Parameters.AddWithValue("@bookId", bookId);
                borrowCommand.ExecuteNonQuery();

                MySqlCommand availableQuantityCommand = new MySqlCommand("select available from books where id=@bookId", connection);
                availableQuantityCommand.Parameters.AddWithValue("@bookId", bookId);
                mySqlDataReader = availableQuantityCommand.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    IDataRecord dataRecord = (IDataRecord)mySqlDataReader;
                    quantity = dataRecord.GetInt32(0);
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
            return quantity;
        }

        public Book returnBook(int userId, int bookId)
        {
            Console.WriteLine("Return book");
            MySqlConnection connection = null;
            MySqlDataReader mySqlDataReader = null;
            Book returned = null;
            try
            {
                connection = new MySqlConnection(myConnectionString);
                connection.Open();

                String changeQuantity = "update books set available=available+1 where id=@bookId";
                MySqlCommand changeQuantityCommand = new MySqlCommand(changeQuantity, connection);
                changeQuantityCommand.Parameters.AddWithValue("@bookId", bookId);
                changeQuantityCommand.ExecuteNonQuery();

                String returnBook = "delete from user_book where user_id=@userId and book_id=@bookId";
                MySqlCommand returnBookCommand = new MySqlCommand(returnBook, connection);
                returnBookCommand.Parameters.AddWithValue("@userID", userId);
                returnBookCommand.Parameters.AddWithValue("@bookId", bookId);
                returnBookCommand.ExecuteNonQuery();

                MySqlCommand selectReturnedBookCommand = new MySqlCommand("select * from books where id=@bookId", connection);
                selectReturnedBookCommand.Parameters.AddWithValue("@bookId", bookId);
                mySqlDataReader = selectReturnedBookCommand.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    IDataRecord dataRecord = (IDataRecord)mySqlDataReader;
                    returned = new Book(dataRecord.GetInt32(0), dataRecord.GetString(1), dataRecord.GetString(2),
                        dataRecord.GetInt32(3));
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
            return returned;
        }
    }
}
