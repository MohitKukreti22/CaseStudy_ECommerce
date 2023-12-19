using E_Commerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_Commerce.DAO
{


    public interface IOrderProcessorRepository
    {
        public bool CreateCustomer(Customer customer);
        public bool CreateProduct(Product product);
        public bool DeleteProduct(int productId);

        public bool DeleteCustomer(int CustomerId);
        /*public bool ProductExists(int productId);

        public bool CustomerExists(int CustomerId);*/

     bool AddToCart(Customer customer, Product product, int OrderQuantity);
        public bool RemoveFromCart(Customer customer, Product product);
        public List<Product> GetAllFromCart(Customer customer);
        public bool PlaceOrder(Customer customer, Dictionary<Product, int> dict, string shippingAddress);

        public List<Product> GetOrdersByCustomer(int customerid);





    }
}