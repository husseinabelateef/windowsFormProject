using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperMarket1
{
    internal class Customer
    {
        public string Name { get; set; }
        static int NumberOfCustomer = 1;
        public string Phone { get; set; }
        public List<Customer_Bill> bills;
        public double Points { get; set; }
        public Customer()
        {
            NumberOfCustomer++;
            bills = new List<Customer_Bill>();
        }
           
        public int ID = NumberOfCustomer; //Auto increment 
    }
}
