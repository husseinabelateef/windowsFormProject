using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperMarket1
{
    internal class Supplier_Bill
    {
        static int NumberOfBills = 0;
        public int BillId { get { return BillId; } private set { BillId = NumberOfBills; } }
        public int SupplierId { get; set; }
        public String SupplierName { get; set; }
        public List<ProductNeed> Supplier_Product = new List<ProductNeed>();
        public double TotalPrice { get; set; }
        public DateTime CreatedTime;
        public Supplier_Bill() 
        {
            NumberOfBills++;
            CreatedTime = new DateTime();
            Supplier_Product = new List<ProductNeed>();
        }

    }
}
