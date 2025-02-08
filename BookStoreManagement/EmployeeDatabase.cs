using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManagement
{
    class EmployeeDatabase
    {
        /// สร้าง database สำหรับรายการรายชื่อพนักงานร้านหนังสือ
        public static void InitializeDatabase()

        {
            using (SqliteConnection bookStoreData = new SqliteConnection($"Filename=bookStoreDatabase.db"))

            {
                bookStoreData.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS employeeDataTable (" +
                    "Employee_ID VARCHAR(8) PRIMARY KEY," +
                    "Employee_FirstName VARCHAR(100) NULL," +
                    "Employee_LastName VARCHAR(100) NULL," +
                    "Employee_Email VARCHAR(100) NULL," +
                    "Password VARCHAR(25) NULL" +
                    ")";

                using (SqliteCommand createTable = new SqliteCommand(tableCommand, bookStoreData))

                {
                    createTable.ExecuteNonQuery();
                }

            }
        }

        /// สร้างฟังก์ชันเพิ่มรหัสพนักงานเเบบอัตโนมัติ
        public static string GenerateEmployeeId(SqliteConnection bookStoreData)
        {
            string lastId = null;


            using (SqliteConnection EmployeeID = new SqliteConnection($"Filename=bookStoreDatabase.db"))
            {
                EmployeeID.Open();

                using (SqliteCommand findLastID_Command = new SqliteCommand(
                    "SELECT Employee_ID FROM employeeDataTable " +
                    "ORDER BY Employee_ID DESC LIMIT 1", EmployeeID))

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
                    return $"A{number:D3}"; // รหัสพนักงานจะเป็น A001, A002,...
                }
                else
                {
                    return "A001";
                }

            }

        }


        /// สร้างฟังก์ชั่นสำหรับเพิ่มรายการรายชื่อพนักงานร้านหนังสือ
        public static void AddEmployee(string inputEmployee_FirstName, string inputEmployee_Lastname, string inputEmail, string inputPassword)
        {

            using (SqliteConnection bookStoreData = new SqliteConnection($"Filename=bookStoreDatabase.db"))
            {
                bookStoreData.Open();

                string newEmployeeID = GenerateEmployeeId(bookStoreData);

                SqliteCommand insertCommand = new SqliteCommand(
                    "INSERT INTO employeeDataTable (" +
                        "Employee_ID," +
                        "Employee_FirstName, " +
                        "Employee_LastName, " +
                        "Employee_Email, " +
                        "Password )" +
                    "VALUES (" +
                        "@Employee_ID," +
                        "@Employee_FirstName, " +
                        "@Employee_LastName, " +
                        "@Employee_Email, " +
                        "@Password);", bookStoreData);

                insertCommand.Parameters.AddWithValue("@Employee_ID", newEmployeeID);
                insertCommand.Parameters.AddWithValue("@Employee_FirstName", inputEmployee_FirstName);
                insertCommand.Parameters.AddWithValue("@Employee_LastName", inputEmployee_Lastname);
                insertCommand.Parameters.AddWithValue("@Employee_Email", inputEmail);
                insertCommand.Parameters.AddWithValue("@Password", inputPassword);

                insertCommand.ExecuteNonQuery();

                bookStoreData.Close();
            }
        }

        /// สร้างฟังก์ชั่นสำหรับยืนยันตัวตนของพนักงานร้านหนังสือ
        public static bool AuthorizeEmployee(string inputEmail, string inputPassword)
        {
            using (SqliteConnection bookStoreData = new SqliteConnection($"Filename=bookStoreDatabase.db"))
            {
                bookStoreData.Open();

                string query = "SELECT COUNT(*) FROM employeeDataTable " +
                                "WHERE " +
                                "Employee_Email = @Employee_Email " +
                                "AND " +
                                "Password = @Password";

                using (SqliteCommand authorizeCommand = new SqliteCommand(query, bookStoreData))
                {
                    authorizeCommand.Parameters.AddWithValue("@Employee_Email", inputEmail);
                    authorizeCommand.Parameters.AddWithValue("@Password", inputPassword);

                    long count = (long)authorizeCommand.ExecuteScalar();
                    return count > 0;
                }
            }
        }

    }
}
