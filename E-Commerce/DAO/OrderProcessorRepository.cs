using E_Commerce.Utility;
using E_Commerce.Entities;
using E_Commerce.Exceptions;
using System;
using System.Collections.Generic;

using Microsoft.Data.SqlClient;
using System.Collections;





namespace E_Commerce.DAO
{
    public class OrderProcessorRepository : IOrderProcessorRepository
    {
        Customer customer = new Customer();
        Product product = new Product();
        Cart carts = new Cart();
        Order order= new Order();
        OrderItem orderItem = new OrderItem();
        public string connectionString;
        SqlCommand cmd = null;

        public OrderProcessorRepository()
        {
            connectionString = DbConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }

        public bool CreateCustomer(Customer customer)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into customers values(@customer_name,@email,@password)";
                cmd.Parameters.AddWithValue("@customer_name", customer.Name);
                cmd.Parameters.AddWithValue("@email", customer.Email);
                cmd.Parameters.AddWithValue("@password", customer.Password);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("\nCustomer Added Successfully............");
                    return true;
                }
            }
            return false;
        }

        public bool CreateProduct(Product product)
        {


            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "Insert into products values(@product_name,@price,@description,@stockQuantity)";
                cmd.Parameters.AddWithValue("@product_name", product.Product_Name);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@description", product.Description);
                cmd.Parameters.AddWithValue("@StockQuantity", product.Quantity);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Product Added Successfully............");
                    return true;
                }
            }
            return false;
        }


        public bool DeleteProduct(int productId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "delete from products where product_id=@product_id";
                cmd.Parameters.AddWithValue("@product_id", productId);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new ProductNotFoundException($"Product with ID {productId} not found.");
                }
                else
                {
                    Console.WriteLine("Product Deleted Successfully");
                }
                return rowsAffected > 0;
               
            }
            }

        public bool DeleteCustomer(int CustomerId)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "delete from customers where customer_id=@customer_id";
                cmd.Parameters.AddWithValue("@customer_id", CustomerId);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new CustomerNotFoundException($"Customer with ID {CustomerId} not found.");
                    
                }
                else
                {

                    Console.WriteLine("Customer Deleted Successfully");
                }
                return rowsAffected > 0;
            }
            
        }


        public bool ProductExists(int productId)
        {
            try {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "SELECT COUNT(*) FROM products WHERE product_id = @product_id";
                    cmd.Parameters.AddWithValue("@product_id", productId);
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read()) {
                        return true;
                    } throw new ProductNotFoundException($"ProductId{productId} does not exists");
                }
            }
            catch(System.Exception e) {

                Console.WriteLine(e.Message);
            }
            return false;


        }
        public bool CustomerExists(int CustomerId)
        {
            try {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "SELECT COUNT(*) FROM customers WHERE customer_id = @customer_id";
                    cmd.Parameters.AddWithValue("@customer_id", CustomerId);
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return true;
                    }
                    throw new CustomerNotFoundException($"CustomerID{CustomerId} does not exists");

                }
            }catch(System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
            }


        public bool AddToCart(Customer customer, Product product, int OrderQuantity)
        {
            
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.Parameters.Clear();

                cmd.CommandText = "INSERT INTO cart VALUES (@customer_id, @product_id, @quantity)";
                {
                    cmd.Parameters.AddWithValue("@customer_id", customer.CustomerId);
                    cmd.Parameters.AddWithValue("@product_id", product.productId);
                    cmd.Parameters.AddWithValue("@quantity", OrderQuantity);
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Product added to cart");
                        return true;
                    }
                    Console.WriteLine("Product Unavialable");
                    return false;

                }
            }


        }




        public bool RemoveFromCart(Customer customer, Product product)
        {
            try
            {

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "delete from cart where customer_id=@customer_id AND product_id=@product_id";
                    cmd.Parameters.AddWithValue("@customer_id", customer.CustomerId);
                    cmd.Parameters.AddWithValue("@product_id", product.productId);

                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new ProductNotFoundException($"Product with ID {product.productId} not found in the cart.");
                    }
                    else
                    {

                        Console.WriteLine("Product Removed Successfully");
                    }



                }
               
            }


            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return true;

        }

        public List<Product> GetAllFromCart(Customer customer)
        {
            List<Product> products = new List<Product>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "select products.product_name,products.price,products.stockQuantity from products " +
                        "join cart on \r\nproducts.product_id=cart.product_id where cart.customer_id=@customerId";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@customerId", customer.CustomerId);
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Product product = new Product();
                        product.Product_Name = (string)reader["product_name"];
                        product.Price = (float)(decimal)reader["price"];
                        product.Quantity = (int)reader["stockQuantity"];
                        products.Add(product);
                    }
                    sqlConnection.Close();
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }


            return products;
        }



        public bool PlaceOrder(Customer customer, Dictionary<Product, int> dict, string shippingAddress)
        {

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    double price = 0;
                    int quantity = 0;
                    int productId = 0;
                    int orderId = 0;


                    foreach (var items in dict)
                    {
                        orderId = 0;
                        productId = items.Key.productId;
                        quantity = items.Value;

                        bool productExist = ProductExists(productId);
                        if (productExist)
                        {
                            cmd.CommandText = "select price*@quantity from products where product_id=@productId";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@quantity", quantity);
                            cmd.Parameters.AddWithValue("@productId", productId);
                            cmd.Connection = sqlConnection;
                            sqlConnection.Open();
                            price = Convert.ToSingle(cmd.ExecuteScalar());
                            sqlConnection.Close();

                            cmd.CommandText = "insert into orders OUTPUT INSERTED.order_id values(@customer_id,@order_date,@total_price,@shipping_address)";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@customer_id", customer.CustomerId);
                            cmd.Parameters.AddWithValue("@order_date", DateTime.Now.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@total_price", price);
                            cmd.Parameters.AddWithValue("@shipping_address", shippingAddress);
                            cmd.Connection = sqlConnection;
                            sqlConnection.Open();
                            orderId = Convert.ToInt32(cmd.ExecuteScalar());
                            sqlConnection.Close();


                            cmd.CommandText = "insert into order_items values(@order_id,@product_id,@quantity_supplied)";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@order_id", orderId);
                            cmd.Parameters.AddWithValue("@product_id", productId);
                            cmd.Parameters.AddWithValue("@quantity_supplied", quantity);
                            cmd.Connection = sqlConnection;
                            sqlConnection.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    if (orderId > 0)
                    {
                        Console.WriteLine($"Order Placed successfully...........");
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                throw new OrderNotFoundException($"Failed to place order for customer with ID {customer.CustomerId}.");
            }

            return false;
        }

        public List<Product> GetOrdersByCustomer(int customerid)
        {
            List<Product> customerProduct = new List<Product>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "select products.*,order_items.quantity_supplied from products join order_items on products.product_id=" +
                        "order_items.product_id\r\njoin orders on orders.order_id=order_items.order_id join customers " +
                        "on customers.customer_id=\r\norders.customer_id where customers.customer_id=@customerid";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@customerid", customerid);
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Product product = new Product();
                        OrderItem orderItem=new OrderItem();
                        product.productId = (int)reader["product_id"];
                        product.Product_Name = (string)reader["product_name"];
                        product.Price = (double)(decimal)reader["price"];
                        product.Quantity = (int)reader["stockQuantity"];
                        product.Description = (string)reader["description"];
                        orderItem.Quantity_Supplied = (int)reader["quantity_supplied"];
                       
                        customerProduct.Add(product);
                    }
                    sqlConnection.Close();

                    if(customerProduct.Count==0)
                    {
                        throw new OrderNotFoundException($"No orders found for customer with ID {customerid}.");
                    }
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }



            return customerProduct;
        }

















    }




}





















  















