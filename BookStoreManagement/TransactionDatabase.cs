using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManagement
{
    class TransactionDatabase
    {
        /// สร้าง database สำหรับบัญชีรายการซื้อขายหนังสือ
        public static void InitializeDatabase()

        {
            using (SqliteConnection bookStoreData = new SqliteConnection($"Filename=bookStoreDatabase.db"))

            {
                bookStoreData.Open();

                String tableCommand = "CREATE TABLE IF NOT EXISTS " +
                    "transactionDataTable (" +
                    "Transaction_NO INTEGER PRIMARY KEY, " +
                    "Bill_NO VARCHAR(6) NULL, " +
                    "Customer_ID VARCHAR(6) NULL, " +
                    "ISBN VARCHAR(13) NULL, " +
                    "Book_Title VARCHAR(100) NULL," +
                    "Quantity INTEGER NULL, " +
                    "Total_Price DECIMAL(10, 2) NULL" +
                    ");";

                using (SqliteCommand createTable = new SqliteCommand(tableCommand, bookStoreData))

                {
                    createTable.ExecuteNonQuery();
                }

            }

        }

        /// สร้างฟังก์ชั่นสำหรับลำดับ Transaction No แบบอัตโนมัติ
        public static int GenerateTransactionNo(SqliteConnection bookStoreData)
        {
            int lastTransaction = 0;

            using (SqliteConnection TransactionNo = new SqliteConnection($"Filename=bookStoreDatabase.db"))
            {
                TransactionNo.Open();

                using (SqliteCommand findLastNo_Command = new SqliteCommand(
                    "SELECT Max(Transaction_No) FROM transactionDataTable", TransactionNo))

                {
                    var result = findLastNo_Command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        lastTransaction = Convert.ToInt32(result);
                    }

                    return lastTransaction + 1;
                }

            }
        }


        /// สร้างฟังก์ชั่นสำหรับเพิ่มรายการขายหนังสือ
        public static void AddTransaction(string inputBill, string inputCustomerId, string inputISBN, string inputTitle, int inputQuantity, decimal inputTotal_Price)
        {

            using (SqliteConnection bookStoreData = new SqliteConnection($"Filename=bookStoreDatabase.db"))
            {
                bookStoreData.Open();

                int inputTransactionNo = GenerateTransactionNo(bookStoreData);

                SqliteCommand insertCommand = new SqliteCommand(
                       "INSERT INTO transactionDataTable (" +

                           "Transaction_NO, " +
                           "Bill_NO, " +
                           "Customer_ID, " +
                           "ISBN, " +
                           "Book_Title, " +
                           "Quantity, " +
                           "Total_Price) " +
                       "VALUES (" +
                           "@Transaction_NO, " +
                           "@Bill_NO, " +
                           "@Customer_ID," +
                           "@ISBN, " +
                           "@Title, " +
                           "@Quantity, " +
                           "@Total_Price);", bookStoreData);

                insertCommand.Parameters.AddWithValue("@Transaction_NO", inputTransactionNo);
                insertCommand.Parameters.AddWithValue("@Bill_NO", inputBill);
                insertCommand.Parameters.AddWithValue("@Customer_ID", inputCustomerId);
                insertCommand.Parameters.AddWithValue("@ISBN", inputISBN);
                insertCommand.Parameters.AddWithValue("@Title", inputTitle);
                insertCommand.Parameters.AddWithValue("@Quantity", inputQuantity);
                insertCommand.Parameters.AddWithValue("@Total_Price", inputTotal_Price);

                insertCommand.ExecuteNonQuery();

                bookStoreData.Close();
            }
        }


        /// สร้างฟังก์ชั่นสำหรับค้นหารายการขายหนังสือ
        public static List<Transaction> SearchTransaction(string fieldName, string inputKeyword)
        {

            List<Transaction> transaction = new List<Transaction>();

            using (SqliteConnection bookStoreData = new SqliteConnection($"Filename=bookStoreDatabase.db"))
            {
                bookStoreData.Open();

                string searchCommand = "SELECT " +
                                        "Bill_NO, " +
                                        "Customer_ID, " +
                                        "ISBN, " +
                                        "Book_Title, " +
                                        "Quantity, " +
                                        "Total_Price " +
                                       "FROM transactionDataTable " +
                                       $"WHERE {fieldName} Like @inputKeyword";

                using (SqliteCommand searchTransactionCommand = new SqliteCommand(searchCommand, bookStoreData))

                {
                    searchTransactionCommand.Parameters.AddWithValue("@inputKeyword", "%" + inputKeyword + "%");

                    using (SqliteDataReader reader = searchTransactionCommand.ExecuteReader())

                    {
                        while (reader.Read())
                        {
                            transaction.Add(new Transaction
                            {
                                Bill_No = reader.GetString(0),
                                Customer_Id = reader.GetString(1),
                                ISBN = reader.GetString(2),
                                Title = reader.GetString(3),
                                Quantity = reader.GetInt32(4),
                                Total_Price = reader.GetDecimal(5)
                            });
                        }
                    }
                }

                bookStoreData.Close();
            }

            return transaction;
        }

        /// สร้างฟังก์ชั่นสำหรับค้นหารายการขายหนังสือด้วยรายการใบเสร็จ
        public static List<Transaction> SearchTransactionByBill(string inputBill)
        {

            return SearchTransaction("Bill_NO", inputBill);
        }

        /// สร้างฟังก์ชั่นสำหรับค้นหารายการขายหนังสือด้วยข้อมูลลูกค้า
        public static List<Transaction> SearchTransactionByCustomer(string inputCustomerId)
        {
            return SearchTransaction("Customer_ID", inputCustomerId);
        }

        /// สร้างฟังก์ชั่นสำหรับเช็คว่ารายการ  BiLL อยู่จริงในฐานข้อมูล
        public static bool BillExist(string inputBill)
        {

            using (SqliteConnection bookStoreData = new SqliteConnection($"Filename=bookStoreDatabase.db"))
            {
                bookStoreData.Open();

                SqliteCommand authorizeCommand = new SqliteCommand(
                    "SELECT COUNT(*) FROM transactionDataTable WHERE " +
                    "Bill_NO = @Bill_NO ", bookStoreData);

                authorizeCommand.Parameters.AddWithValue("@Bill_NO", inputBill);

                long count = (long)authorizeCommand.ExecuteScalar();

                return count > 0;
            }
        }


        public static bool BillVerify(string inputBill, string inputCustomerId, string inputISBN)
        {

            using (SqliteConnection bookStoreData = new SqliteConnection($"Filename=bookStoreDatabase.db"))
            {
                bookStoreData.Open();

                SqliteCommand verifyCommand = new SqliteCommand(
                    "SELECT COUNT(*) FROM transactionDataTable WHERE " +
                    "Bill_NO = @Bill_NO AND " +
                    "Customer_ID = @Customer_ID AND " +
                    "ISBN = @ISBN", bookStoreData);

                verifyCommand.Parameters.AddWithValue("@Bill_NO", inputBill);
                verifyCommand.Parameters.AddWithValue("@Customer_ID", inputCustomerId);
                verifyCommand.Parameters.AddWithValue("@ISBN", inputISBN);

                long count = (long)verifyCommand.ExecuteScalar();

                return count > 0;
            }
        }


        //// สร้างฟังก์ชั่นสำหรับเปลี่ยนแปลงรายการขายหนังสือด้วยข้อมูลลูกค้า
        //public static void UpdateTransaction(string inputCustomerId, string newISBN, int newQuantity, decimal newTotal_Price)
        //{

        //    using (SqliteConnection bookStoreData = new SqliteConnection($"Filename=bookStoreDatabase.db"))
        //    {
        //        bookStoreData.Open();

        //        SqliteCommand updateCommand = new SqliteCommand(
        //            "UPDATE transactionDataTable" +
        //            "SET ISBN = @ISBN," +
        //            "Quantity = @Quantity," +
        //            "Total_Price = @Total_Price" +
        //            "WHERE Customer_ID = @Customer_ID", bookStoreData);

        //        updateCommand.Parameters.AddWithValue("@Customer_ID", inputCustomerId);
        //        updateCommand.Parameters.AddWithValue("@ISBN", newISBN);
        //        updateCommand.Parameters.AddWithValue("@Quantity", newQuantity);
        //        updateCommand.Parameters.AddWithValue("@Total_Price", newTotal_Price);

        //        updateCommand.ExecuteNonQuery();

        //        bookStoreData.Close();
        //    }
        //}


        /// สร้างฟังก์ชั่นสำหรับลบรายการขายหนังสือ
        public static void DeleteTransaction(string inputBill, string inputCustomerId, string inputISBN)
        {

            using (SqliteConnection bookStoreData = new SqliteConnection($"Filename=bookStoreDatabase.db"))
            {
                bookStoreData.Open();

                SqliteCommand deleteCommand = new SqliteCommand(
                    "DELETE FROM transactionDataTable " +
                    "WHERE Bill_NO = @Bill_NO AND " +
                    "Customer_ID = @Customer_ID AND " +
                    "ISBN = @ISBN", bookStoreData);

                deleteCommand.Parameters.AddWithValue("@Bill_NO", inputBill);
                deleteCommand.Parameters.AddWithValue("@Customer_ID", inputCustomerId);
                deleteCommand.Parameters.AddWithValue("@ISBN", inputISBN);

                deleteCommand.ExecuteNonQuery();

                bookStoreData.Close();
            }
        }

        public static List<Transaction> ShowTransactionList()
        {
            List<Transaction> transactions = new List<Transaction>();

            using (SqliteConnection bookStoreData = new SqliteConnection($"Filename=bookStoreDatabase.db"))
            {
                bookStoreData.Open();

                SqliteCommand selectCommand = new SqliteCommand(
                    "SELECT " +
                    "Bill_NO," +
                    "Customer_ID, " +
                    "ISBN, " +
                    "Book_Title, " +
                    "Quantity, " +
                    "Total_Price " +
                    "FROM transactionDataTable ", bookStoreData);

                using (SqliteDataReader reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Transaction transaction = new Transaction
                        {
                            Bill_No = reader["Bill_NO"].ToString(),
                            Customer_Id = reader["Customer_ID"].ToString(),
                            ISBN = reader["ISBN"].ToString(),
                            Title = reader["Book_Title"].ToString(),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            Total_Price = Convert.ToDecimal(reader["Total_Price"])

                        };

                        transactions.Add(transaction);
                    }

                }
                bookStoreData.Close();
            }

            return transactions;
        }
    }
}