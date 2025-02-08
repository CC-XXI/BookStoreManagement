using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookStoreManagement
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        public SignUpWindow()
        {
            InitializeComponent();
        }

        private void Sign_Up_Click(object sender, RoutedEventArgs e)
        {
            string inputEmployee_FirstName = first_Name_txt.Text;
            string inputEmployee_Lastname = last_Name_txt.Text;
            string inputEmail = email_txt.Text;
            string inputPassword = Password_txt.Text;

            //เช็คการ input ข้อมูล

            if (string.IsNullOrWhiteSpace(inputEmployee_FirstName) ||
                string.IsNullOrWhiteSpace(inputEmployee_Lastname) ||
                string.IsNullOrWhiteSpace(inputEmail) ||
                string.IsNullOrWhiteSpace(inputPassword))
            {
                MessageBox.Show("Name, Email and password cannot be empty.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // เพิ่มรายการรายชื่อพนักงานร้านหนังสือ

            EmployeeDatabase.AddEmployee(inputEmployee_FirstName, inputEmployee_Lastname, inputEmail, inputPassword);

            MessageBox.Show("Add Employee successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            first_Name_txt.Text = string.Empty;
            last_Name_txt.Text = string.Empty;
            email_txt.Text = string.Empty;
            Password_txt.Text = string.Empty;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
        }


    }
}
