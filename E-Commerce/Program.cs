using System;
using System.Threading.Channels;
using System.Xml.Linq;
using E_Commerce.DAO;
using E_Commerce.Entities;

Customer customer=new Customer();
Product product=new Product();
Cart cart=new Cart();   

IOrderProcessorRepository impl= new OrderProcessorRepository();
int choice = 0;
int i = 1;


do
{
    Console.WriteLine("------------------------------------------------------Happy Shopping---------------------------------------------------\n\n" +   
    "Press 1: Register Customer\n\n" +
    "Press 2: Create Product\n\n" +
    "Press 3: Delete Product\n\n" +
    "Press 4: Add Product to Cart\n\n" +
    "Press 5: Remove Product From  Cart\n\n" +
    "Press 6: Show Cart\n\n"+
    "Press 7: Place Order\n\n" +
    "Press 8: Get Order By CustomerID\n\n" +



    "Press 0 to Exit");
    choice = Convert.ToInt32(Console.ReadLine());
    switch (choice)
    {
        case 0:
            i = 0;
            break;
        case 1:
            Console.WriteLine("Enter your name:");
            string customerName = Console.ReadLine();
            customer.Name = customerName;
            Console.WriteLine("Enter your Email:");
            string email = Console.ReadLine();
            customer.Email = email;
            Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();
            customer.Password = password;
            impl.CreateCustomer(customer);
            break;

        case 2:
            Console.WriteLine("Enter your ProductName:");
            string productName = Console.ReadLine();
            product.Product_Name = productName;
            Console.WriteLine("Enter your price:");
            double price =double.Parse(Console.ReadLine());
            product.Price = price;
            Console.WriteLine("Enter your description:");
            string description = Console.ReadLine();
            product.Description = description;
            Console.WriteLine("Enter your Quantity:");
            int quantity = int.Parse(Console.ReadLine());
            product.Quantity = quantity;
            impl.CreateProduct(product);
            break;

        case 3:
            Console.WriteLine("Enter id to delete product:");
            int productId = Convert.ToInt32(Console.ReadLine());
            impl.DeleteProduct(productId);
            break;


          


        case 4:
            
            Console.WriteLine("Enter customerId");
            int CustomerID = Convert.ToInt32(Console.ReadLine());
            customer.CustomerId = CustomerID;
            Console.WriteLine("Enter ProductID");
            int ProductID = Convert.ToInt32(Console.ReadLine());
            product.productId= ProductID;
            Console.WriteLine("enter Quantity ");
            int Quantity = Convert.ToInt32(Console.ReadLine());
            cart.OrderQuantity = Quantity;
            impl.AddToCart(customer, product,Quantity);

            break;


            case 5:
            Console.WriteLine("Enter customerId");
            int customerID = Convert.ToInt32(Console.ReadLine());
            customer.CustomerId = customerID;
            Console.WriteLine("Enter ProductID");
            int productID = Convert.ToInt32(Console.ReadLine());
            product.productId = productID;
            impl.RemoveFromCart(customer, product);
            break;


        case 6:

            List<Product> productsInCart = new List<Product>();
            Console.WriteLine("Enter customer Id to view cart:");
           int customerId = Convert.ToInt32(Console.ReadLine());
            customer.CustomerId = customerId;
            productsInCart = impl.GetAllFromCart(customer);
            foreach (var items in productsInCart)
            {
                Console.WriteLine(items);
            }
            break;



        case 7:
            Console.WriteLine("Enter customerId:");
            customerId = Convert.ToInt32(Console.ReadLine());
            customer.CustomerId = customerId;
            Console.WriteLine("Enter productId:");
            productId = Convert.ToInt32(Console.ReadLine());
            product.productId = productId;
            Console.WriteLine("Enter quantity");
            quantity = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter shipping address");
            string address = Console.ReadLine();

            Dictionary<Product, int> dict = new Dictionary<Product, int>();
            dict.Add(product, quantity);
            impl.PlaceOrder(customer, dict, address);
            break;

        case 8:
            List<Product> customerproducts= new List<Product>();
            Console.WriteLine("Enter Customer ID to view Orders:");
            int customerid=Convert.ToInt32(Console.ReadLine());
            customer.CustomerId = customerid;
            customerproducts = impl.GetOrdersByCustomer(customerid);
            foreach (var item in customerproducts)
            {
                Console.WriteLine(item);
            }
            break;

       

        default:
            Console.WriteLine("Invalid Inputs....");
            break;
    }
} while (i > 0);