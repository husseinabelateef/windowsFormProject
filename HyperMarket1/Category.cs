using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperMarket1
{
    internal class Category
    {  
        public string Name { get; set; }
        //C-TwoDigit
        static int numberOfCategory = 1;
        public int ID { get { return ID; } private set { this.ID = numberOfCategory; } }
        public List<Product> Products { get; set; }
        public Category()
        {
            numberOfCategory++;
        }
        public Category(string Name, int ID)
        {
            numberOfCategory++;
            this.Name = Name;
            this.ID = ID;
        }
        public override string ToString()
        {
            return $"{Name}{ID}";
        }
    }
}
