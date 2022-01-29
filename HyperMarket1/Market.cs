using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperMarket1
{
    internal sealed class Market
    {
        //singlton ISA 
        public double Budget { get; set; }
        public string Name { get; set; }
        public List<Category> Categories;
        public List<Customer> Customers;
        public List<Supplier> Suppliers;
        public List<Product> Products; // allExisting and none existing products
        public List<Cashier> Cashiers;
        private Market()
        {
            Budget = 1500000;
            Name = "United Group";
            Categories = new List<Category>();
            Customers = new List<Customer>();
            Suppliers = new List<Supplier>();
            this.Products = new List<Product>();
            Cashiers = new List<Cashier>();

        }
        private static Market mark = null;
        public static Market market
        {
            get
            {
                if (mark == null)
                {
                    mark = new Market();
                }
                return mark;
            }
        }
        public List<Product> ReportOExistingProducts()
        {
            List<Product> products = new List<Product>();
            foreach (Category cate in Categories)
            {
                foreach (Product item in cate.Products)
                {
                    products.Add(item);
                }
            }
            return products;
        }
        public List<Product> ReportOfExpireProducts()
        {
            List<Product> products = new List<Product>();
            foreach (Category cate in Categories)
            {
                foreach (Product item in cate.Products)
                {
                    if (item.ExpireDate < DateTime.Now)
                        products.Add(item);
                }
            }
            return products;
        }

    }
}
