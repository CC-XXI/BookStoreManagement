using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookStoreManagement
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            EmployeeDatabase.InitializeDatabase();
            BookDatabase.InitializeDatabase();
            CustomerDatabase.InitializeDatabase();
            TransactionDatabase.InitializeDatabase();
        }

        private void Log_In_Click(object sender, RoutedEventArgs e)
        {
            string inputEmail = Email_txt.Text;
            string inputPassword = Password_txt.Password;

            //เช็คการ input ข้อมูล

            if (string.IsNullOrWhiteSpace(inputEmail) || string.IsNullOrWhiteSpace(inputPassword))
            {
                MessageBox.Show("Email and password cannot be empty.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //เช็คข้อมูล email เเละ password ในระบบ

            if (EmployeeDatabase.AuthorizeEmployee(inputEmail, inputPassword))
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                ControlWindow controlwindow = new ControlWindow();
                controlwindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid email or password. Please try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Sign_Up_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow signupwindow = new SignUpWindow();
            signupwindow.Show();
            this.Close();
        }
    }

}