using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManagement
{
    class CustomerDatabase
    {
        /// สร้าง database สำหรับรายการลูกค้า
        public static void InitializeDatabase()

        {
            using (SqliteConnection bookStoreData = new SqliteConnection($"Filename=bookStoreDatabase.db"))

            {
                bookStoreData.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS customerDataTable (" +
                    "Customer_ID VARCHAR(6) PRIMARY KEY," +
                    "Customer_FirstName VARCHAR(100) NULL," +
                    "Customer_LastName VARCHAR(100) NULL," +
                    "Customer_Address VARCHAR(2048) NULL," +
                    "Customer_Email VARCHAR(100) NULL" +
                    ")";

                using (SqliteCommand createTable = new SqliteCommand(tableCommand, bookStoreData))

                {
                    createTable.ExecuteNonQuery();
                }

            }

        }

        /// สร้างฟังก์ชั่นสำหรับรหัสลูกค้าแบบอัตโนมัติ
        public static string GenerateCustomerId(SqliteConnection bookStoreData)
        {
            string lastId = null;


            using (SqliteConnection CustomerID = new SqliteConnection($"Filename=bookStoreDatabase.db"))
            {
                CustomerID.Open();

                using (SqliteCommand findLastID_Command = new SqliteCommand(
                    "SELECT Customer_ID FROM customerDataTable " +
                    "ORDER BY Customer_ID DESC LIMIT 1", CustomerID))

                {
                    var result = findLastID_Command.ExecuteScalar();
                    if (result != null)
                    {
                        lastId = result.ToString();
                    }
                }

                if (lastId != null)
                {
                    int number = int.Parse(lastId.Substring(1));
                    number++;
                    return $"C{number:D5}"; // รหัสพนักงานจะเป็น C00001, C00002,...
                }
                else
                {
                    return "C00001";
                }

            }

        }

        /// สร้างฟังก์ชั่นสำหรับเพิ่มรายการลูกค้า
        public static void AddCustomer(string inputCustomer_FirstName, string inputCustomer_Lastname, string inputAddress, string inputEmail)
        {

            using (SqliteConnection bookStoreData = new SqliteConnection($"Filename=bookStoreDatabase.db"))
            {
                bookStoreData.Open();

                string newCustomerID = GenerateCustomerId(bookStoreData);

                SqliteCommand insertCommand = new SqliteCommand(
                    "INSERT INTO customerDataTable (" +
                        "Customer_ID," +
                        "Customer_FirstName, " +
                        "Customer_LastName, " +
                        "Customer_Address, " +
                        "Customer_Email) " +
                    "VALUES (" +
                        "@Customer_ID," +
                        "@Customer_FirstName, " +
                        "@Customer_LastName, " +
                        "@Customer_Address, " +
                        "@Customer_Email);", bookStoreData);

                insertCommand.Parameters.AddWithValue("@Customer_ID", newCustomerID);
                insertCommand.Parameters.AddWithValue("@Customer_FirstName", inputCustomer_FirstName);
                insertCommand.Parameters.AddWithValue("@Customer_LastName", inputCustomer_Lastname);
                insertCommand.Parameters.AddWithValue("@Customer_Address", inputAddress);
                insertCommand.Parameters.AddWithValue("@Customer_Email", inputEmail);               

                insertCommand.ExecuteNonQuery();

                bookStoreData.Close();
            }
        }

        /// สร้างฟังก์ชั่นสำหรับค้นหารายการลูกค้า
        public static List<Customer> SearchCustomer(string fieldName, string inputKeyword)
        {
            
            List<Customer> customers = new List<Customer>();

            using (SqliteConnection bookStoreData = new SqliteConnection($"Filename=bookStoreDatabase.db"))
            {
               bookStoreData.Open();

                string searchCommand = "SELECT " +
                                         "Customer_ID," +
                                         "Customer_FirstName, " +
                                         "Customer_LastName, " +
                                         "Customer_Address, " +
                                         "Customer_Email " +
                                       "FROM customerDataTable " +
                                       $"WHERE {fieldName} Like @inputKeyword";

                using (SqliteCommand searchCustomerCommand = new SqliteCommand(searchCommand, bookStoreData))

                {
                    searchCustomerCommand.Parameters.AddWithValue("@inputKeyword", "%" + inputKeyword + "%");

                    using (SqliteDataReader reader = searchCustomerCommand.ExecuteReader())

                    {
                        while (reader.Read())
                        {
                            customers.Add(new Customer
                            {
                                Customer_Id = reader.GetString(0),
                                Customer_FirstName = reader.GetString(1),
                                Customer_LastName = reader.GetString(2),
                                Address = reader.GetString(3),
                                Email = reader.GetString(4)
                            });
                        }
                    }
                }

                bookStoreData.Close();
            }
            
            return customers;
        }

        /// สร้างฟังก์ชั่นสำหรับค้นหารายการลูกค้าด้วย Customer ID
        public static List<Customer> SearchCustomerByID(string inputCustomerID)
        {

            return SearchCustomer("Customer_ID", inputCustomerID);
        }

        /// สร้างฟังก์ชั่นสำหรับค้นหารายการลูกค้าด้วยชื่อ
        public static List<Customer> SearchCustomerByFirstName(string inputCustomer_FirstName)
        {

            return SearchCustomer("Customer_FirstName", inputCustomer_FirstName);
        }

        /// สร้างฟังก์ชั่นสำหรับค้นหารายการลูกค้าด้วยนามสกุล
        public static List<Customer> SearchCustomerByLastName(string inputCustomer_LastName)
        {

            return SearchCustomer("Customer_LastName", inputCustomer_LastName);
        }

        /// สร้างฟังก์ชั่นสำหรับค้นหารายการลูกค้าด้วยที่อยู่
        public static List<Customer> SearchCustomerByAddress(string inputAddress)
        {

            return SearchCustomer("Customer_Address", inputAddress);
        }

        /// สร้างฟังก์ชั่นสำหรับค้นหารายการลูกค้าด้วยอีเมล์
        public static List<Customer> SearchCustomerByEmail(string inputEmail)
        {

            return SearchCustomer("Customer_Email", inputEmail);
        }




        /// สร้างฟังก์ชั่นสำหรับเปลี่ยนแปลงรายการลูกค้า
        public static void UpdateCustomer(string inputCustomerId, string newCustomer_FirstName, string newCustomer_Lastname, string newAddress, string newEmail)
        {

            using (SqliteConnection bookStoreData = new SqliteConnection($"Filename=bookStoreDatabase.db"))
            {
                bookStoreData.Open();

                SqliteCommand updateCommand = new SqliteCommand(
                    "UPDATE customerDataTable " +
                    "SET Customer_FirstName = @Customer_FirstName, " +
                    "Customer_LastName = @Customer_LastName, " +
                    "Customer_Address = @Customer_Address, " +
                    "Customer_Email = @Customer_Email " +
                    "WHERE Customer_ID = @Customer_ID", bookStoreData);

                 updateCommand.Parameters.AddWithValue("@Customer_ID", inputCustomerId);
                 updateCommand.Parameters.AddWithValue("@Customer_FirstName", newCustomer_FirstName);
                 updateCommand.Parameters.AddWithValue("@Customer_LastName", newCustomer_Lastname);
                 updateCommand.Parameters.AddWithValue("@Customer_Address", newAddress);
                 updateCommand.Parameters.AddWithValue("@Customer_Email", newEmail);
                        
                 updateCommand.ExecuteNonQuery();

                bookStoreData.Close();
            }
        }


        /// สร้างฟังก์ชั่นสำหรับลบรายการลูกค้า
        public static void DeleteCustomer(string inputCustomerId)
        {

            using (SqliteConnection bookStoreData = new SqliteConnection($"Filename=bookStoreDatabase.db"))
            {
                bookStoreData.Open();

                SqliteCommand deleteCommand = new SqliteCommand(
                    "DELETE FROM customerDataTable " +
                    "WHERE Customer_ID = @Customer_ID", bookStoreData);

                deleteCommand.Parameters.AddWithValue("@Customer_ID", inputCustomerId);

                deleteCommand.ExecuteNonQuery();

                bookStoreData.Close();
            }
        }

        /// สร้างฟังก์ชันสำหรับแสดงรายการลูกค้าบนระบบ
        public static List<Customer> ShowCustomerList()
        {
            List<Customer> customers = new List<Customer>();

            using (SqliteConnection bookStoreData = new SqliteConnection($"Filename=bookStoreDatabase.db"))
            {
                bookStoreData.Open();
                SqliteCommand selectCommand = new SqliteCommand(
                    "SELECT " +
                    "Customer_ID," +
                    "Customer_FirstName, " +
                    "Customer_LastName, " +
                    "Customer_Address, " +
                    "Customer_Email " +
                    "FROM customerDataTable ", bookStoreData);

                using (SqliteDataReader reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer
                        {
                            Customer_Id = reader["Customer_ID"].ToString(),
                            Customer_FirstName = reader["Customer_FirstName"].ToString(),
                            Customer_LastName = reader["Customer_LastName"].ToString(),
                            Address = reader["Customer_Address"].ToString(),
                            Email = reader["Customer_Email"].ToString()
                        };

                        customers.Add(customer);
                    }

                }
                bookStoreData.Close();
            }

            return customers;
        }

        /// สร้างฟังก์ชั่นสำหรับเช็คว่ารายการหนังสือมีอยู่จริงในฐานข้อมูล
        public static bool CustomerExist(string inputCustomerId)
        {

            using (SqliteConnection bookStoreData = new SqliteConnection($"Filename=bookStoreDatabase.db"))
            {
                bookStoreData.Open();

                SqliteCommand authorizeCommand = new SqliteCommand(
                    "SELECT COUNT(*) FROM customerDataTable WHERE " +
                    "Customer_ID = @Customer_ID ", bookStoreData);

                authorizeCommand.Parameters.AddWithValue("@Customer_ID", inputCustomerId);

                long count = (long)authorizeCommand.ExecuteScalar();

                return count > 0;
            }
        }


    }

}