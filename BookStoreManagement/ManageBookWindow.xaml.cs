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
    /// Interaction logic for ManageBookWindow.xaml
    /// </summary>
    public partial class ManageBookWindow : Window
    {
        public ManageBookWindow()
        {
            InitializeComponent();

            //แสดงรายการหนังสือในช่องรายการรวม
            List<Book> booksShowList = BookDatabase.ShowBookList();
            BooksListView.ItemsSource = booksShowList;

        }

        // เลือกรายการหนังสือในช่องรายการรวมจะมาเเสดงในช่องรับข้อมูลหนังสือ
        private void BooksListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BooksListView.SelectedItem is Book selectedBook)
            {
                ISBN_txt.Text = selectedBook.ISBN;
                book_Title_txt.Text = selectedBook.Title;
                book_Description_txt.Text = selectedBook.Description;
                book_Price_decimal.Text = selectedBook.Price.ToString();
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            string inputISBN = ISBN_txt.Text;
            string inputTitle = book_Title_txt.Text;
            string inputDescription = book_Description_txt.Text;
            string inputPriceText = book_Price_decimal.Text;

            //เช็คการ input ข้อมูล

            if (string.IsNullOrWhiteSpace(inputISBN) || 
                string.IsNullOrWhiteSpace(inputTitle) ||
                string.IsNullOrWhiteSpace(inputDescription) ||
                string.IsNullOrWhiteSpace(inputPriceText))
            {
                MessageBox.Show("Input information cannot be empty.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // เช็ต input ของราคาว่าเป็นตัวเลขหรือไม่

            if (!decimal.TryParse(inputPriceText, out decimal inputPrice))
            {
                MessageBox.Show("Input a valid price.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // เช็ค ISBN ว่าซ้ำกับรายการี่มีอยู่เเล้วหรือไม่
            if (BookDatabase.BookExist(inputISBN))
            {
                MessageBox.Show("This ISBN already exists.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;

            }

            else
            {

                BookDatabase.AddBook(inputISBN, inputTitle, inputDescription, inputPrice);

                MessageBox.Show("Add Book successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                List<Book> booksShowList = BookDatabase.ShowBookList();
                BooksListView.ItemsSource = booksShowList;

                ISBN_txt.Clear();
                book_Title_txt.Clear();
                book_Description_txt.Clear();
                book_Price_decimal.Clear();
            }

        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {

            string inputISBN = ISBN_txt.Text;

            // เช็ต input ของ ISBN

            if (string.IsNullOrWhiteSpace(inputISBN))
            {
                MessageBox.Show("Input ISBN cannot be empty.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // เช็ค ISBN ว่าซ้ำกับรายการที่มีอยู่เเล้วหรือไม่
            if (BookDatabase.BookExist(inputISBN))

            {
                BookDatabase.DeleteBook(inputISBN);

                MessageBox.Show("Delete Book successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                List<Book> booksShowList = BookDatabase.ShowBookList();
                BooksListView.ItemsSource = booksShowList;

            }

            else
            {
                MessageBox.Show("This ISBN is not exists.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


        }

        private void update_Click(object sender, RoutedEventArgs e)
        {

            string inputISBN = ISBN_txt.Text;
            string newTitle = book_Title_txt.Text;
            string newDescription = book_Description_txt.Text;
            string newPriceText = book_Price_decimal.Text;

            //เช็คการ input ข้อมูล

            if (string.IsNullOrWhiteSpace(inputISBN) ||
                string.IsNullOrWhiteSpace(newTitle) ||
                string.IsNullOrWhiteSpace(newDescription) ||
                string.IsNullOrWhiteSpace(newPriceText))
            {
                MessageBox.Show("Input information cannot be empty.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            // เช็ต input ของราคาว่าเป็นตัวเลขหรือไม่

            if (!decimal.TryParse(newPriceText, out decimal newPrice))
            {
                MessageBox.Show("Input a valid price.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            else

            {

                BookDatabase.UpdateBook(inputISBN, newTitle, newDescription, newPrice);

                MessageBox.Show("Update Book successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                List<Book> booksShowList = BookDatabase.ShowBookList();
                BooksListView.ItemsSource = booksShowList;

                ISBN_txt.Clear();
                book_Title_txt.Clear();
                book_Description_txt.Clear();
                book_Price_decimal.Clear();
            }

        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            string inputISBN = ISBN_txt.Text;
            string inputTitle = book_Title_txt.Text;
            string inputDescription = book_Description_txt.Text;
            string inputPriceText = book_Price_decimal.Text;


            //เช็คการ input ข้อมูล
            if (string.IsNullOrWhiteSpace(inputISBN) &&
                string.IsNullOrWhiteSpace(inputTitle) &&
                string.IsNullOrWhiteSpace(inputDescription) &&
                string.IsNullOrWhiteSpace(inputPriceText))
            {
                MessageBox.Show("Input information cannot be empty.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // เช็คข้อมูลแต่ละช่องเพื่อทำการค้นหา
            List<Book> allSearchResults = new List<Book>();

            if (!string.IsNullOrWhiteSpace(inputISBN))
            {
                allSearchResults.AddRange(BookDatabase.SearchBookByISBN(inputISBN));
            }

            if (!string.IsNullOrWhiteSpace(inputTitle))
            {
                allSearchResults.AddRange(BookDatabase.SearchBookByTitle(inputTitle));
            }

            if (!string.IsNullOrWhiteSpace(inputDescription))
            {
                allSearchResults.AddRange(BookDatabase.SearchBookByDescription(inputDescription));
            }

            if (!string.IsNullOrWhiteSpace(inputPriceText))
            {
                if (decimal.TryParse(inputPriceText, out decimal price))
                {
                    allSearchResults.AddRange(BookDatabase.SearchBookByPrice(inputPriceText));
                }
                else 
                {
                    MessageBox.Show("Input a valid price.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            if (allSearchResults.Count>0)
            {
                var uniqueResults = allSearchResults.GroupBy(b => new {b.ISBN, b.Title, b.Description, b.Price})
                    .SelectMany(g => g.Distinct())
                    .ToList();

                if (uniqueResults.Count > 0) 
                {
                    BooksListView.ItemsSource = uniqueResults;
                }

            }
            else

            {
                MessageBox.Show("Not found in Book Database.", "Matching Error", MessageBoxButton.OK, MessageBoxImage.Warning);
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
