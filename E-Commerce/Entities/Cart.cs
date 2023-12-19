using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities
{
    internal class Cart
    {

        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public int productId { get; set; }
        public int OrderQuantity { get; set; }
    }

  
    }

