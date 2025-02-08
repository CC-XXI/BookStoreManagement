using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
    /// Interaction logic for ManageTransactionWindow.xaml
    /// </summary>
    public partial class ManageTransactionWindow : Window
    {
        public ManageTransactionWindow()
        {
            InitializeComponent();

            //แสดงรายการขายหนังสือในช่องรายการรวม
            List<Transaction> transactionsShowList = TransactionDatabase.ShowTransactionList();
            orderedBookDataListView.ItemsSource = transactionsShowList;
        }

        private void orderedBookDataListView_SelectionChanged(object sender, SelectionChangedEventArgs e)

        {
            if (orderedBookDataListView.SelectedItem is Transaction selectTransaction)
            {
                customer_ID_txt.Text = selectTransaction.Customer_Id;
                bill_No_txt.Text = selectTransaction.Bill_No;
                ISBN_txt.Text = selectTransaction.ISBN;
                book_Title_txt.Text = selectTransaction.Title;
                quantity_int.Text = selectTransaction.Quantity.ToString();
                price_decimal.Text = selectTransaction.Total_Price.ToString(); 
            }

        }

        /// สร้างฟังก์ชั่นสำหรับให้รายการหนังสือขึ้นมาอัตโนมัติเมื่อเติม ISBN ของหนังสือ

        private Book purchasedBook;

        private void ISBN_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            string inputISBN = ISBN_txt.Text;

            if (!string.IsNullOrEmpty(inputISBN))
            {
                var bookDetails = BookDatabase.ShowBookListByISBN(inputISBN);

                if (bookDetails.Count > 0)
                {
                    purchasedBook = bookDetails[0];
                    book_Title_txt.Text = purchasedBook.Title;
                    price_decimal.Text = purchasedBook.Price.ToString();
                }
                else
                {
                    purchasedBook = null;
                    book_Title_txt.Text = string.Empty;
                    price_decimal.Text = string.Empty;

                    MessageBox.Show("Input ISBN is not exists", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
        }


        private void add_Click(object sender, RoutedEventArgs e)
        {

            string inputCustomerId = customer_ID_txt.Text;
            string inputBill = bill_No_txt.Text;
            string inputISBN = ISBN_txt.Text;
            string inputTitle = book_Title_txt.Text;
            string inputPriceText = price_decimal.Text;
            string inputQuantityText = quantity_int.Text;

            //เช็คการ input ข้อมูล

            if (string.IsNullOrWhiteSpace(inputCustomerId) ||
                string.IsNullOrWhiteSpace(inputBill) ||
                string.IsNullOrWhiteSpace(inputISBN) ||
                string.IsNullOrWhiteSpace(inputTitle) ||
                string.IsNullOrWhiteSpace(inputPriceText) ||
                string.IsNullOrWhiteSpace(inputQuantityText))
            {
                MessageBox.Show("Input information cannot be empty", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // เช็ตว่า input ราคาหนังสือว่าเป็นเลขจำนวหตัวเลขและตรงกับในฐานข้อมูลรือไม่

            if (!decimal.TryParse((inputPriceText), out decimal inputPrice) ||
                inputPrice != purchasedBook.Price)
            {
                MessageBox.Show("Input correct book price.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // เช็ตว่า input จำนวนหนังสือว่าเป็นเลขจำนวนเต็มหรือไม่

            if (!int.TryParse((inputQuantityText), out int inputQuantity) ||
                inputQuantity <=0)
            {
                MessageBox.Show("Input a valid quantity.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // เช็ค Customer ID ว่าซ้ำกับรายการที่มีอยู่เเล้วหรือไม่
            if (!CustomerDatabase.CustomerExist(inputCustomerId))
            {
                MessageBox.Show("Input Customer is not exists.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            decimal inputTotal_Price = inputQuantity * purchasedBook.Price;

   
            TransactionDatabase.AddTransaction(inputBill, inputCustomerId, inputISBN, inputTitle, inputQuantity, inputTotal_Price);
            
            MessageBox.Show("Add Transaction successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            List<Transaction> transactionsShowList = TransactionDatabase.ShowTransactionList();
            orderedBookDataListView.ItemsSource = transactionsShowList;

            customer_ID_txt.Clear();
            bill_No_txt.Clear();
            ISBN_txt.Clear();
            book_Title_txt.Clear();
            price_decimal.Clear();
            quantity_int.Clear();

        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            string inputCustomerId = customer_ID_txt.Text;
            string inputBill = bill_No_txt.Text;
            string inputISBN = ISBN_txt.Text;

            //เช็คการ input ข้อมูล

            if (string.IsNullOrWhiteSpace(inputCustomerId) &&
                string.IsNullOrWhiteSpace(inputBill) &&
                string.IsNullOrWhiteSpace(inputISBN))
            {
                MessageBox.Show("Input information cannot be empty", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //เช็คว่ารายการ bill มีอยู่จริงในฐานข้อมูล
            if (!TransactionDatabase.BillExist(inputBill))

            {
                MessageBox.Show("Input Bill No. is not exists.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            // เช็ตว่า input ข้อมูลตรงกับรายการ bill กับข้อมูลการขายหนังสือและลูกค้า ตรงกันในฐานข้อม฿ล
            if (!TransactionDatabase.BillVerify(inputBill, inputCustomerId, inputISBN))

            {
                MessageBox.Show("Input information does not match with the database.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            else
            {
                TransactionDatabase.DeleteTransaction(inputBill, inputCustomerId, inputISBN);

                MessageBox.Show("Delete Transaction successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                List<Transaction> transactionsShowList = TransactionDatabase.ShowTransactionList();
                orderedBookDataListView.ItemsSource = transactionsShowList;

                customer_ID_txt.Clear();
                bill_No_txt.Clear();
                ISBN_txt.Clear();
                book_Title_txt.Clear();
                price_decimal.Clear();
                quantity_int.Clear();
            }


        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            string inputCustomerId = customer_ID_txt.Text;
            string inputBill = bill_No_txt.Text;

            //เช็คการ input ข้อมูล
            if (string.IsNullOrWhiteSpace(inputCustomerId) &&
                string.IsNullOrWhiteSpace(inputBill))

            {
                MessageBox.Show("Input information cannot be empty.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // เช็คข้อมูลแต่ละช่องเพื่อทำการค้นหา
            List<Transaction> allSearchResults = new List<Transaction>();

            if (!string.IsNullOrWhiteSpace(inputCustomerId))
            {
                allSearchResults.AddRange(TransactionDatabase.SearchTransactionByCustomer(inputCustomerId));
            }

            if (!string.IsNullOrWhiteSpace(inputBill))
            {
                allSearchResults.AddRange(TransactionDatabase.SearchTransactionByBill(inputBill));
            }


            if (allSearchResults.Count>0)
            {
                var uniqueResults = allSearchResults.GroupBy(b => new { b.Bill_No, b.Customer_Id })
                    .SelectMany(g => g.Distinct())
                    .ToList();

                if (uniqueResults.Count > 0)
                {
                    orderedBookDataListView.ItemsSource = uniqueResults;

                }
            }
            else

            {
                MessageBox.Show("Not found in Transaction Database.", "Matching Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }

        private void check_Out_Click(object sender, RoutedEventArgs e)
        {
            string inputBill = bill_No_txt.Text;

            //เช็คการ input ข้อมูล
            if (string.IsNullOrWhiteSpace(inputBill))

            {
                MessageBox.Show("Input Bill No. cannot be empty", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //เช็คว่ารายการ bill มีอยู่จริงในฐานข้อมูล
            if (!TransactionDatabase.BillExist(inputBill))

            {
                MessageBox.Show("Input Bill No. is not exists.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            BillWindow billwindow = new BillWindow(inputBill);
            billwindow.Show();

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            ControlWindow controlwindow = new ControlWindow();
            controlwindow.Show();
            this.Close();
        }


    }
}