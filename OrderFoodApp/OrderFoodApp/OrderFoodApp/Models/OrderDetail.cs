using System;
using System.Collections.Generic;
using System.Text;

namespace OrderFoodApp.Models
{
    public class OrderDetail
    {
        public int id { get; set; }
        public double price { get; set; }
        public int qty { get; set; }
        public double totalAmount { get; set; }
        public int orderId { get; set; }
        public int productId { get; set; }
        public string productname { get; set; }
        public string sumary { get; set; }
        public string image { get; set; }
    }
}
