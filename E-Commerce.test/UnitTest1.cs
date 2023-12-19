using E_Commerce.Entities;
using E_Commerce.DAO;

using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using E_Commerce.Exceptions;



namespace E_Commerce.test


{
    public class Tests
    {
        private const string connectionString = "Server=LAPTOP-117H7Q83;Database=ECOM; Integrated Security=True;TrustServerCertificate=True";
        #region Test Create Product
        [Test]
        public void TestToCreaProduct()
        {
            OrderProcessorRepository repository = new OrderProcessorRepository();
            repository.connectionString = connectionString;

            bool addProduct = repository.CreateProduct(new Product
            {

                Product_Name = "IPHONE",
                Price = 80500,
                Description = "High Qualtiy Camera",

                Quantity = 10
            });
            Assert.IsTrue(addProduct);
        }
        #endregion

        #region aDD to cart
        [Test]
        public void TestAddToCartSuccess()
        {
            Customer customer = new Customer { CustomerId = 1 }; 
            Product product = new Product { productId = 3 }; 
            int orderQuantity = 2;

            OrderProcessorRepository repository = new OrderProcessorRepository();
            repository.connectionString = connectionString;
            bool result = repository.AddToCart(customer, product, orderQuantity);

            Assert.IsTrue(result);

        


        }
        #endregion
        #region 
        [Test]
        public void TestCustomerNotFoundException()
        {
            OrderProcessorRepository repo = new OrderProcessorRepository();

            Assert.Throws<CustomerNotFoundException>(() => repo.DeleteCustomer(12));
        }
        #endregion

        #region  
        [Test]
        public void TestProductNotFoundException()
        {
            OrderProcessorRepository repo = new OrderProcessorRepository();

            Assert.Throws<ProductNotFoundException>(() => repo.DeleteProduct(21));
        }
        #endregion
    }
}







        /* #region Exception
         [Test]
         public void AddToCart_ProductNotFound()
         {
             // Arrange
            int  productId = 1 ; 


             OrderProcessorRepository repository = new OrderProcessorRepository();
             repository.connectionString = connectionString;

             // Assert
             Assert.Throws<E_Commerce.Exceptions.ProductNotFoundException>(() => repository.ProductExists(productId));

         }
         #endregion*/



