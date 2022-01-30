using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperMarket1
{
    internal class Cashier
    {
        public string Name { get; set; }
        static int NumberOfCashiers = 0;
        public int ID { get; set; } 
        public string Username { get; set; } 
        public string Password { get; set; }
        public int WorkingHours { get; set; }
        public string Phone { get; set; }
        public float Salary { get; set; }

        //public List<ProductNeed> Products=new List<ProductNeed>();
        //Check For Product's Existance
        public Cashier() //:this($"hamdy{this.ID}" ,$"cashier{this.ID}" ,"root" , 12 , new List<string>{ "0100005558"}  , 2500.0)
        {
            NumberOfCashiers++;
        }
        public Cashier(string Name, String UserName, String Password, int WorkingHours, string PPhone, float Salary)
        {
            NumberOfCashiers++;
            this.Name = Name;
            this.WorkingHours = WorkingHours;
            this.Salary = Salary;
            this.Password = Password;
            this.Username = UserName;
            this.Phone = PPhone;
            this.ID = NumberOfCashiers;
        }

        //check existance of product search 
        public bool ProductIsExist(Product product, Category category)
        {
            foreach (Product p in category.Products)
            {
                if (p.ID == product.ID)
                {
                    return true;
                }

            }
            return false;
        }

        // check existance of product and it's amount covered the customer Need
        private Category retCat(string catName)
        {
            foreach (Category item in Market.market.Categories)
            {
                if (item.ToString() == catName)
                {
                    return item;
                }
            }
            return null;
        }
        public bool ProductCoverd(ProductNeed product, Category category)
        {
            foreach (Product p in category.Products)
            {
                if (p.ID == product.Product.ID)
                {
                    if (p.Amount >= product.AmountNeeded)
                        return true;
                }

            }
            return false;
        }

        //check if customer is not exist in customer List or not before adding
        public void AddCustomer(Customer customer)
        {
            if (!Market.market.Customers.Contains(customer))
            {
                Market.market.Customers.Add(customer);
            }

            int index = Market.market.Customers.IndexOf(customer);
            customer = Market.market.Customers[index];
        }
        //create Customer Bill
        public void CreateBill(Customer customer)
        {
            Customer_Bill customer_Bill = new Customer_Bill();
            customer_Bill.CashierId = this.ID;
            customer_Bill.CustomerId = customer.ID;
            customer_Bill.CashierName = this.Name;
            customer_Bill.CustomerName = customer.Name;
            customer.bills.Add(customer_Bill);
        }


        //edit amount of existance product
        public void EditProduct(ProductNeed pnew, Customer customer)
        {

            if (pnew.AmountNeeded > 0 && ProductCoverd(pnew, retCat(pnew.Product.category)))
            {
                //int index = customer.bills[customer.bills.Count - 1].Customer_Product.IndexOf(pnew);
                DeleteProduct(pnew, customer);
                customer.bills[customer.bills.Count - 1].Customer_Product.Add(pnew);
            }
            else
            {
                DeleteProduct(pnew, customer);
            }
        }

        //adding product to the last added  cutomer's bill and throw exception if 
        // the product couldn't cover amount or not exist

        public void AddProduct(ProductNeed p, Customer customer)
        {
            ProductNeed check = p;
            //check if the product is already exist in customer basket
            if (customer.bills[customer.bills.Count - 1].Customer_Product.Contains(p)) // 
            {
                int index = customer.bills[customer.bills.Count - 1].Customer_Product.IndexOf(p);

                check.AmountNeeded += customer.bills[customer.bills.Count - 1].Customer_Product[index].AmountNeeded;

                if (this.ProductCoverd(check, retCat(check.Product.category)))
                {
                    DeleteProduct(p, customer);
                    customer.bills[customer.bills.Count - 1].Customer_Product.Add(check);
                }
                else
                {
                    throw new Exception("product Not cover");
                }
            }
            // for first time adding product to our basket
            else if (this.ProductCoverd(p, retCat(p.Product.category)))
            {
                customer.bills[customer.bills.Count - 1].Customer_Product.Add(p);
            }
            else
                throw new Exception("product Not cover");

        }
        //Remove product to the last added  cutomer's bill 
        public void DeleteProduct(ProductNeed p, Customer customer)
        {
            customer.bills[customer.bills.Count - 1].Customer_Product.Remove(p);
        }

        //remove that bill "لو الكستمر رجع ف كلامه"

        public void FinalDeletForBill(Customer customer)
        {
            customer.bills.Remove(customer.bills[customer.bills.Count - 1]);
        }

        //Complete the payment basket list of product every prodct have amount and price 
        // and decrease amount of stock products in Market.market
        public void pay(Customer customer)
        {
            //increase budget 
            double total = 0;
            foreach (ProductNeed p in customer.bills[customer.bills.Count - 1].Customer_Product)
            {
                total += (p.AmountNeeded * p.Product.PriceForBuy);
            }
            customer.bills[customer.bills.Count - 1].TotalPrice = total;

            Market.market.Budget += total;

            // decrease amount of products
            foreach (ProductNeed item in customer.bills[customer.bills.Count - 1].Customer_Product)
            {
                int catIndex = Market.market.Categories.IndexOf(retCat(item.Product.category));
                int ProdIndex = Market.market.Categories[catIndex].Products.IndexOf(item.Product);
                Market.market.Categories[catIndex].Products[ProdIndex].Amount -= item.AmountNeeded;

            }
            //calculate customer Points
            double revenu = 0;
            foreach (ProductNeed item in customer.bills[customer.bills.Count - 1].Customer_Product)
            {
                revenu += (item.Product.PriceForSell - item.Product.PriceForBuy);
            }
            customer.Points += revenu * (double)0.01; //points is 10% of revenue 

        }


    }
}
