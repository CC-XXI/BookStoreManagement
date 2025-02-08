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
    /// Interaction logic for BillWindow.xaml
    /// </summary>
    public partial class BillWindow : Window
    {
        public BillWindow(string inputBill)
        {
            InitializeComponent();

            date_txt.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            decimal grand_Total = 0;

            List<Transaction> printBill = TransactionDatabase.SearchTransactionByBill(inputBill);

            if (printBill.Count>0)
            {

                transactionStackPanel.Children.Clear();

                foreach (var orderlist in printBill)
                {
                    customer_ID_txt.Text = orderlist.Customer_Id;
                    bill_No_txt.Text = orderlist.Bill_No;

                    Grid transactionGrid = new Grid
                    {
                        Margin = new Thickness(0, 5, 0, 5)
                    };

                    transactionGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                    transactionGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(40) });  
                    transactionGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });  

                    TextBlock titleTextBlock = new TextBlock
                    {
                        Text = orderlist.Title,
                        Margin = new Thickness(0, 0, 10, 0),
                        FontSize = 10
                    };

                    TextBlock quantityTextBlock = new TextBlock
                    {
                        Text = $"{orderlist.Quantity}",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(0, 0, 10, 0),
                        FontSize = 10
                    };

                    TextBlock priceTextBlock = new TextBlock
                    {
                        Text = $"{orderlist.Total_Price:F2}",
                        HorizontalAlignment = HorizontalAlignment.Right,
                        Margin = new Thickness(0, 0, 10, 0),
                        FontSize = 10
                    };

                    grand_Total += orderlist.Total_Price;
                    grand_Total_decimal.Text = $"{grand_Total:F2}";

                    Grid.SetColumn(titleTextBlock, 0);
                    Grid.SetColumn(quantityTextBlock, 1);
                    Grid.SetColumn(priceTextBlock, 2);

                    transactionGrid.Children.Add(titleTextBlock);
                    transactionGrid.Children.Add(quantityTextBlock);
                    transactionGrid.Children.Add(priceTextBlock);

                    transactionStackPanel.Children.Add(transactionGrid);
                }

            }
        }
    }
}
