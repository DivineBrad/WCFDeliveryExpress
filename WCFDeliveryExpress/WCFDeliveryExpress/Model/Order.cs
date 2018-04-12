using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryExpress.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime DateTime { get; set; }
        public string OrderNo { get; set; }
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public decimal Subtotal { get; set; }
        public OrderStatus Status { get; set; }
        public List<Item> Items { get; set; }
        public Customer Customer { get; set; } 
    }
}
