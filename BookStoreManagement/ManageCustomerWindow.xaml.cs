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
    /// Interaction logic for ManageCustomerWindow.xaml
    /// </summary>
    public partial class ManageCustomerWindow : Window
    {
        public ManageCustomerWindow()
        {
            InitializeComponent();

            //แสดงรายการลูกค้าในช่องรายการรวม
            List<Customer> customersShowList = CustomerDatabase.ShowCustomerList();
            CustomersListView.ItemsSource = customersShowList;
        }

        private void CustomersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomersListView.SelectedItem is Customer selectedCustomer)
            {
                customer_ID_txt.Text = selectedCustomer.Customer_Id;
                first_Name_txt.Text = selectedCustomer.Customer_FirstName;
                last_Name_txt.Text = selectedCustomer.Customer_LastName;
                adresss_txt.Text = selectedCustomer.Address;
                email_txt.Text = selectedCustomer.Email;
            }

        }


        private void add_Click(object sender, RoutedEventArgs e)
        {
            string inputCustomerId = customer_ID_txt.Text;
            string inputCustomer_FirstName = first_Name_txt.Text;
            string inputCustomer_LastName = last_Name_txt.Text;
            string inputAddress = adresss_txt.Text;
            string inputEmail = email_txt.Text;

            //เช็คการ input ข้อมูล

            if (string.IsNullOrWhiteSpace(inputCustomer_FirstName) ||
                string.IsNullOrWhiteSpace(inputCustomer_LastName) ||
                string.IsNullOrWhiteSpace(inputAddress) ||
                string.IsNullOrWhiteSpace(inputEmail))
            {
                MessageBox.Show("Input information cannot be empty expect Customer ID space.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!string.IsNullOrWhiteSpace(inputCustomerId)) 
            {
                MessageBox.Show("Please leave the Customer ID space blank. It will be generated automatically.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            else
            {

                CustomerDatabase.AddCustomer(inputCustomer_FirstName, inputCustomer_LastName, inputAddress, inputEmail);

                MessageBox.Show("Add Customer successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                List<Customer> customersShowList = CustomerDatabase.ShowCustomerList();
                CustomersListView.ItemsSource = customersShowList;

                customer_ID_txt.Clear();
                first_Name_txt.Clear();
                last_Name_txt.Clear();
                adresss_txt.Clear();
                email_txt.Clear();
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            string inputCustomerId = customer_ID_txt.Text;

            // เช็ต input ของ Customer ID

            if (string.IsNullOrWhiteSpace(inputCustomerId))
            {
                MessageBox.Show("Input Customer ID cannot be empty.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // เช็ค Customer ID ว่าซ้ำกับรายการที่มีอยู่เเล้วหรือไม่
            if (CustomerDatabase.CustomerExist(inputCustomerId))

            {
                CustomerDatabase.DeleteCustomer(inputCustomerId);

                MessageBox.Show("Delete Customer successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                List<Customer> customersShowList = CustomerDatabase.ShowCustomerList();
                CustomersListView.ItemsSource = customersShowList;

            }

            else
            {
                MessageBox.Show("Input Customer is not exists.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            string inputCustomerId = customer_ID_txt.Text;
            string newCustomer_FirstName = first_Name_txt.Text;
            string newCustomer_LastName = last_Name_txt.Text;
            string newAddress = adresss_txt.Text;
            string newEmail = email_txt.Text;

            //เช็คการ input ข้อมูล

            if (string.IsNullOrWhiteSpace(inputCustomerId) ||
                string.IsNullOrWhiteSpace(newCustomer_FirstName) ||
                string.IsNullOrWhiteSpace(newCustomer_LastName) ||
                string.IsNullOrWhiteSpace(newAddress) ||
                string.IsNullOrWhiteSpace(newEmail))
            {
                MessageBox.Show("Input information cannot be empty.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            else
            {
                CustomerDatabase.UpdateCustomer(inputCustomerId, newCustomer_FirstName, newCustomer_LastName, newAddress, newEmail);

                MessageBox.Show("Update Customer successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                List<Customer> customersShowList = CustomerDatabase.ShowCustomerList();
                CustomersListView.ItemsSource = customersShowList;

                customer_ID_txt.Clear();
                first_Name_txt.Clear();
                last_Name_txt.Clear();
                adresss_txt.Clear();
                email_txt.Clear();
            }

        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            string inputCustomerId = customer_ID_txt.Text;
            string inputCustomer_FirstName = first_Name_txt.Text;
            string inputCustomer_LastName = last_Name_txt.Text;
            string inputAddress = adresss_txt.Text;
            string inputEmail = email_txt.Text;


            //เช็คการ input ข้อมูล
            if (string.IsNullOrWhiteSpace(inputCustomerId) &&
                string.IsNullOrWhiteSpace(inputCustomer_FirstName) &&
                string.IsNullOrWhiteSpace(inputCustomer_LastName) &&
                string.IsNullOrWhiteSpace(inputAddress) &&
                string.IsNullOrWhiteSpace(inputEmail))
            {
                MessageBox.Show("Input information cannot be empty.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // เช็คข้อมูลแต่ละช่องเพื่อทำการค้นหา
            List<Customer> allSearchResults = new List<Customer>();

            if (!string.IsNullOrWhiteSpace(inputCustomerId))
            {
                allSearchResults.AddRange(CustomerDatabase.SearchCustomerByID(inputCustomerId));
            }

            if (!string.IsNullOrWhiteSpace(inputCustomer_FirstName))
            {
                allSearchResults.AddRange(CustomerDatabase.SearchCustomerByFirstName(inputCustomer_FirstName));
            }

            if (!string.IsNullOrWhiteSpace(inputCustomer_LastName))
            {
                allSearchResults.AddRange(CustomerDatabase.SearchCustomerByLastName(inputCustomer_LastName));
            }

            if (!string.IsNullOrWhiteSpace(inputAddress))
            {
                allSearchResults.AddRange(CustomerDatabase.SearchCustomerByAddress(inputAddress));
            }

            if (!string.IsNullOrWhiteSpace(inputEmail))
            {
                allSearchResults.AddRange(CustomerDatabase.SearchCustomerByEmail(inputEmail));
            }


            if (allSearchResults.Count>0)
            {
                var uniqueResults = allSearchResults.GroupBy(b => new { b.Customer_Id, b.Customer_FirstName, b.Customer_LastName, b.Address, b.Email })
                    .SelectMany(g => g.Distinct())
                    .ToList();

                if (uniqueResults.Count > 0)
                {
                    CustomersListView.ItemsSource = uniqueResults;
                }

            }
            else

            {
                MessageBox.Show("Not found in Customer Database.", "Matching Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }


        private void back_Click(object sender, RoutedEventArgs e)
        {
            ControlWindow controlwindow = new ControlWindow();
            controlwindow.Show();
            this.Close();
        }

    }
}
