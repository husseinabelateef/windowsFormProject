using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperMarket1
{
    internal class Manager
    { 
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int ID { get; set; }


        public Manager()
        {
            Name = "Admin";
            UserName = "Admin";
            Password = "Admin";
            ID = 255;
        }
        //add new Cashier
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
        public void AddCashier(Cashier Cashier)
        {
            if (!Market.market.Cashiers.Contains(Cashier))
            {
                Market.market.Cashiers.Add(Cashier);
            }
        }
        // add new Supplier
        public void AddSupplier(Supplier supplier)
        {
            if (!SupplierIsExist(supplier))
            {
                Market.market.Suppliers.Add(supplier);
            }
        }

        //Add category to categories if is not exist
        public void AddCategory(Category category)
        {
            if (!CategoryIsExist(category))
            {
                Market.market.Categories.Add(category);
            }
        }

        //create Supplier Bill
        public void CreateBill(Supplier supplier)
        {
            Supplier_Bill Supplier_Bill = new Supplier_Bill();
            Supplier_Bill.SupplierId = supplier.ID;
            Supplier_Bill.SupplierName = supplier.Name;
            supplier.bills.Add(Supplier_Bill);
        }

        //Remove Supplier
        public void DeleteSupplier(Supplier supplier)
        {
            if (SupplierIsExist(supplier))
                Market.market.Suppliers.Remove(supplier);
        }

        //Remove category
        public void DeleteCategory(Category category)
        {
            if (CategoryIsExist(category))
                Market.market.Categories.Remove(category);
        }

        //Search For Supplier
        public bool SupplierIsExist(Supplier supplier)
        {
            return Market.market.Suppliers.Contains(supplier);
        }

        //search for category
        public bool CategoryIsExist(Category category)
        {
            return Market.market.Categories.Contains(category);
        }

        //Add product to supplier-list
        public void AddSupplierProduct(Supplier supplier, ProductNeed product)
        {
            int lastBillIndex = supplier.bills.Count - 1;
            // check of this product exist in supplier bill then add new amount
            if (supplier.bills[lastBillIndex].Supplier_Product.Contains(product))
            {
                ProductNeed che = product;
                int ProductIndex = supplier.bills[lastBillIndex].Supplier_Product.IndexOf(product);
                che.AmountNeeded += supplier.bills[lastBillIndex].Supplier_Product[ProductIndex].AmountNeeded;
                RemoveSupplierProduct(supplier, product);
                AddSupplierProduct(supplier, che); //call back to function and do else statment
            }
            else
            {
                double oldTotalPrice = supplier.bills[lastBillIndex].TotalPrice;

                double newTotalPrice = 0;

                newTotalPrice += product.AmountNeeded * product.Product.PriceForBuy;
                if (CheckBudgedCovered(newTotalPrice))
                {
                    supplier.bills[lastBillIndex].Supplier_Product.Add(product);
                    supplier.bills[lastBillIndex].TotalPrice = newTotalPrice;
                }
                else
                {
                    //warning if the budget
                    throw new Exception("budget not cover this amount of products");
                }

            }
        }

        //Remove product from supplier bill
        public void RemoveSupplierProduct(Supplier supplier, ProductNeed product)
        {
            int lastBillIndex = supplier.bills.Count - 1;
            supplier.bills[lastBillIndex].Supplier_Product.Remove(product);

            int productIndex = supplier.bills[lastBillIndex].Supplier_Product.IndexOf(product);

            int amountNeeded = supplier.bills[lastBillIndex].Supplier_Product[productIndex].AmountNeeded;

            double priceOfbuy = supplier.bills[lastBillIndex].Supplier_Product[productIndex].Product.PriceForBuy;

            double priceOfRemovedProduct = priceOfbuy * (double)amountNeeded;

            supplier.bills[lastBillIndex].TotalPrice -= priceOfRemovedProduct;

        }
        //check budget covered 
        public bool CheckBudgedCovered(double billprice)
        {
            double total = 0;
            return ((Market.market.Budget - billprice) >= 0) ? true : false;
        }
        //paymnet to supplier
        public void pay(Supplier supplier)
        {
            //decrease Budget amount
            int lastBillIndex = supplier.bills.Count - 1;
            Market.market.Budget -= supplier.bills[lastBillIndex].TotalPrice;

            //increase amount of product in Category
            foreach (ProductNeed item in supplier.bills[lastBillIndex].Supplier_Product)
            {
                Category cat = retCat(item.Product.category);
                int indexofcat = Market.market.Categories.IndexOf(cat);
                int indexOfProduct = Market.market.Categories[indexofcat].Products.IndexOf(item.Product);
                Market.market.Categories[indexofcat].Products[indexOfProduct].Amount += item.AmountNeeded;
                Market.market.Categories[indexofcat].Products[indexOfProduct].IsExist = true;

            }


        }
    }
}
