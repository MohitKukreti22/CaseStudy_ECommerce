using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace E_Commerce.Entities
{

    public class Product


    {
        public int productId { get; set; }
        public String Product_Name { get; set; }
        public double Price { get; set; }
        public String Description { get; set; }

        public int Quantity { get; set; }




        public override string ToString()
        {
            return $"ProductID: {productId}\nProductName: {Product_Name}\nPrice: {Price}\nQuantity: {Quantity}\nDescription:{Description}";
        }
    }
}
