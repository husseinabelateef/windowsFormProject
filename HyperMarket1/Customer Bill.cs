using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperMarket1
{
    internal class Customer_Bill
    {
        static int NumberOfBills = 0;
        public int BillId { get { return BillId; } private set { BillId = NumberOfBills; } }
        public int CustomerId { get; set; }
        public String CustomerName { get; set; }
        public int CashierId { get; set; }
        public string CashierName { get; set; }
        public List<ProductNeed> Customer_Product = new List<ProductNeed>();
        public double TotalPrice { get; set; }
        public DateTime CreatedTime;
        public Customer_Bill()
        {
            NumberOfBills++;
            CreatedTime = new DateTime();
            Customer_Product = new List<ProductNeed>();  
             
        } 
    }
}
