using Microsoft.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO.Packaging;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManagement
{
    class BookDatabase
    {
        /// สร้าง database สำหรับรายการหนังสือ
        public static void InitializeDatabase()

        {
            using (SqliteConnection bookStoreData = new SqliteConnection($"Filename=bookStoreDatabase.db"))

            {
                bookStoreData.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS bookDataTable (ISBN VARCHAR(13) PRIMARY KEY," +
                    "Book_Title VARCHAR(100) NULL," +
                    "Book_Description VARCHAR(2048) NULL," +
                    "Book_Price DECIMAL(2) NULL)";

                using (SqliteCommand createTable = new SqliteCommand(tableCommand, bookStoreData))

                {
                    createTable.ExecuteNonQuery();
                }

            }

        }

        /// สร้างฟังก์ชั่นสำหรับเพิ่มรายการหนังสือ
        public static void AddBook(string inputISBN, string inputTitle, string inputDescription, Decimal inputPrice)
        {

            using (SqliteConnection bookStoreData = new SqliteConnection($"Filename=bookStoreDatabase.db"))
            {
                bookStoreData.Open();

                SqliteCommand insertCommand = new SqliteCommand(
                    "INSERT INTO bookDataTable (" +
                        "ISBN, " +
                        "Book_Title, " +
                        "Book_Description, " +
                        "Book_Price) " +
                    "VALUES (" +
                        "@ISBN, " +
                        "@Title, " +
                        "@Description, " +
                        "@Price);", bookStoreData);

                insertCommand.Parameters.AddWithValue("@ISBN", inputISBN);
                insertCommand.Parameters.AddWithValue("@Title", inputTitle);
                insertCommand.Parameters.AddWithValue("@Description", inputDescription);
                insertCommand.Parameters.AddWithValue("@Price", inputPrice);

                insertCommand.ExecuteNonQuery();

                bookStoreData.Close();
            }
        }

        /// สร้างฟังก์ชันสำหรับแสดงรายการหนังสือบนระบบ
        public static List<Book> ShowBookList()
        {
            List<Book> books = new List<Book>();

            using (SqliteConnection bookStoreData = new SqliteConnection($"Filename=bookStoreDatabase.db"))
            {
                bookStoreData.Open();
                SqliteCommand selectCommand = new SqliteCommand(
                    "SELECT " +
                    "ISBN, " +
                    "Book_Title, " +
                    "Book_Description, " +
                    "Book_Price " +
                    "FROM bookDataTable", bookStoreData);

                using (SqliteDataReader reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book book = new Book
                        {
                            ISBN = reader["ISBN"].ToString(),
                            Title = reader["Book_Title"].ToString(),
                            Description = reader["Book_Description"].ToString(),
                            Price = Convert.ToDecimal(reader["Book_Price"])
                        };

                        books.Add(book);
                    }

                }
                bookStoreData.Close();
            }

            return books;
        }



        /// สร้างฟังก์ชั่นสำหรับค้นหารายการหนังสือ
        public static List<Book> SearchBook(string fieldName, string inputKeyword)
        {

            List<Book> books = new List<Book>();

            using (SqliteConnection bookStoreData = new SqliteConnection($"Filename=bookStoreDatabase.db"))
            {
                bookStoreData.Open();

                string searchCommand = $"SELECT " +
                                         "ISBN, " +
                                         "Book_Title, " +
                                         "Book_Description, " +
                                         "Book_Price " +
                                       "FROM bookDataTable " +
                                       $"WHERE {fieldName} Like @inputKeyword";  

                using (SqliteCommand searchBooksCommand = new SqliteCommand(searchCommand, bookStoreData))

                {
                    searchBooksCommand.Parameters.AddWithValue("@inputKeyword", "%" + inputKeyword + "%");

                    using (SqliteDataReader reader = searchBooksCommand.ExecuteReader())

                    {
                        while (reader.Read())
                        {
                            books.Add(new Book
                            {
                                ISBN = reader.GetString(0),
                                Title = reader.GetString(1),
                                Description = reader.GetString(2),
                                Price = reader.GetDecimal(3)
                            });
                        }
                    }
                }

                bookStoreData.Close();
            }

            return books;
        }

        /// สร้างฟังก์ชั่นสำหรับค้นหารายการหนังสือด้วย ISBN
        public static List<Book> SearchBookByISBN(string inputISBN)
        {

            return SearchBook("ISBN", inputISBN);
        }

        /// สร้างฟังก์ชั่นสำหรับค้นหารายการหนังสือด้วยชื่อหนังสือ
        public static List<Book> SearchBookByTitle(string inputTitle)
        {

            return SearchBook("Book_Title", inputTitle);
        }

        /// สร้างฟังก์ชั่นสำหรับค้นหารายการหนังสือด้วยเนื้อหาคำอธิบายของหนังสือ
        public static List<Book> SearchBookByDescription(string inputDescription)
        {

            return SearchBook("Book_Description", inputDescription);
        }

        /// สร้างฟังก์ชั่นสำหรับค้นหารายการหนังสือด้วยเนื้อหาคำอธิบายของหนังสือ
        public static List<Book> SearchBookByPrice(string inputPrice)
        {

            return SearchBook("Book_Price", inputPrice);
        }


        /// สร้างฟังก์ชั่นสำหรับเปลี่ยนแปลงรายการหนังสือ
        public static void UpdateBook(string inputISBN, string newTitle, string newDescription, Decimal newPrice)
        {

            using (SqliteConnection bookStoreData = new SqliteConnection($"Filename=bookStoreDatabase.db"))
            {
                bookStoreData.Open();

                SqliteCommand updateCommand = new SqliteCommand(
                    "UPDATE bookDataTable " +
                    "SET Book_Title = @Title, " +
                    "Book_Description = @Description, " +
                    "Book_Price = @Price " +
                    "WHERE ISBN = @ISBN", bookStoreData);
                    
                updateCommand.Parameters.AddWithValue("@ISBN", inputISBN);
                updateCommand.Parameters.AddWithValue("@Title", newTitle);
                updateCommand.Parameters.AddWithValue("@Description", newDescription);
                updateCommand.Parameters.AddWithValue("@Price", newPrice);

                updateCommand.ExecuteNonQuery();

                bookStoreData.Close();
            }
        }


        /// สร้างฟังก์ชั่นสำหรับลบรายการหนังสือ
        public static void DeleteBook(string inputISBN)
        {

            using (SqliteConnection bookStoreData = new SqliteConnection($"Filename=bookStoreDatabase.db"))
            {
                bookStoreData.Open();

                SqliteCommand deleteCommand = new SqliteCommand(
                    "DELETE FROM bookDataTable WHERE " +
                    "ISBN = @ISBN", bookStoreData);

                deleteCommand.Parameters.AddWithValue("@ISBN", inputISBN);

                deleteCommand.ExecuteNonQuery();

                bookStoreData.Close();
            }
        }

        /// สร้างฟังก์ชั่นสำหรับเช็คว่ารายการหนังสือมีอยู่จริงในฐานข้อมูล
        public static bool BookExist(string inputISBN)
        {

            using (SqliteConnection bookStoreData = new SqliteConnection($"Filename=bookStoreDatabase.db"))
            {
                bookStoreData.Open();

                SqliteCommand authorizeCommand = new SqliteCommand(
                    "SELECT COUNT(*) FROM bookDataTable WHERE " +
                    "ISBN = @ISBN ", bookStoreData);

                authorizeCommand.Parameters.AddWithValue("@ISBN", inputISBN);

                long count = (long)authorizeCommand.ExecuteScalar();
                
                return count > 0;
            }
        }

        // สร้างฟังก์ชั่นสำหรับโชว์รายการหนังสือตาม ISBN
        public static List<Book> ShowBookListByISBN(String inputISBN)
        {
            List<Book> books = new List<Book>();

            using (SqliteConnection bookStoreData = new SqliteConnection($"Filename=bookStoreDatabase.db"))
            {
                bookStoreData.Open();
                SqliteCommand selectCommand = new SqliteCommand(
                    "SELECT " +
                    "ISBN, " +
                    "Book_Title, " +
                    "Book_Price " +
                    "FROM bookDataTable " +
                    "WHERE ISBN = @ISBN ", bookStoreData);

                selectCommand.Parameters.AddWithValue("@ISBN", inputISBN);

                using (SqliteDataReader reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book book = new Book
                        {
                            ISBN = reader["ISBN"].ToString(),
                            Title = reader["Book_Title"].ToString(),
                            Price = Convert.ToDecimal(reader["Book_Price"])
                        };

                        books.Add(book);
                    }

                }
                bookStoreData.Close();
            }

            return books;
        }
    }
}