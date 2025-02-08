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
    /// Interaction logic for ControlWindow.xaml
    /// </summary>
    public partial class ControlWindow : Window
    {
        public ControlWindow()
        {
            InitializeComponent();
        }

        private void book_Manage_Click(object sender, RoutedEventArgs e)
        {
            ManageBookWindow manageBookWindow = new ManageBookWindow();
            manageBookWindow.Show();
            this.Close();
        }

        private void customer_Manage_Click(object sender, RoutedEventArgs e)
        {
            ManageCustomerWindow manageCustomerwindow = new ManageCustomerWindow();
            manageCustomerwindow.Show();
            this.Close();
        }

        private void transaction_Manage_Click(object sender, RoutedEventArgs e)
        {
            ManageTransactionWindow manageTransactionwindow = new ManageTransactionWindow();
            manageTransactionwindow.Show();
            this.Close();
        }

        private void Log_Out_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
        }
    }
}
