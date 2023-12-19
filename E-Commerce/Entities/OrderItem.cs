using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities
{
    public class OrderItem
    {

        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity_Supplied { get; set; }


        public override string ToString()
        {
            return $"Quantity_Supplied: {Quantity_Supplied}\n";
        }
    }
}
