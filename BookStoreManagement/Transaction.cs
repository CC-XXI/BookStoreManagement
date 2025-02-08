using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManagement
{
    class Transaction
    {
        public int TransactionNo { get; set; }
        public string Bill_No { get; set; }
        public string Customer_Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public int Quantity  { get; set; }
        public decimal Total_Price { get; set; }
    }
}
