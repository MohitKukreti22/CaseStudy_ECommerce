using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Exceptions
{
    
        public class OrderNotFoundException : Exception
        {
            public OrderNotFoundException() : base("Order not found.")
            {
            }

            public OrderNotFoundException(string message) : base(message)
            {
            }

            public OrderNotFoundException(string message, Exception innerException) : base(message, innerException)
            {
            }
        }
    }
