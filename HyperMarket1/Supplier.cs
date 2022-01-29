using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperMarket1
{
    internal class Supplier
    {

        public string Name { get; set; }
        // S-TwoDigits 
        static int NumberOfSupplier = 1;
        public int ID { get { return ID; } private set { ID = NumberOfSupplier; } }
        public List<string> PhoneNumbers;
        public List<Supplier_Bill> bills;
        public Category category;
        public Supplier()
        {
            NumberOfSupplier++;
            PhoneNumbers = new List<string>();
            bills = new List<Supplier_Bill>();
            category = new Category();
            Name = "hamdy";
        }
        public void Addingproduct(string category, string name, double priceForSell)
        {
            Product produ = Market.market.Products.Find(x => x.Name == name);
            if (produ != null)
            {
                Product product = new Product(produ.Name, produ.PriceForSell, produ.category, produ.ID);
                this.category.Products.Add(product);
            }
            else
            {
                Product product = new Product(name, priceForSell, category);
                this.category.Products.Add(product);
                Market.market.Products.Add(product);
            }

        }
        //remove product from this supplier list and checking if there is another supplier 
        //provide this product or not to take action of removing it from market
        //product or not
        public void RemoveProduct(string category, string name, double priceForSell)
        {
            //Product produ = Market.market.Products.Find(x => x.Name == name);
            Product supProduct = this.category.Products.Find(x => x.Name == name);
            if (supProduct != null)
            {
                this.category.Products.Remove(supProduct);
                bool existInAnotherSupp = false;
                foreach (Supplier supplier in Market.market.Suppliers)
                {
                    if (supplier.ID != this.ID)
                    {
                        if (supplier.category.Products.Contains(supProduct))
                        {
                            existInAnotherSupp = true;
                        }
                    }
                }
                if (!existInAnotherSupp)
                    Market.market.Products.Remove(supProduct);
            }


        }
    }
}
