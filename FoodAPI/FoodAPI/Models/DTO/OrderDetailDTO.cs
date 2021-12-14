using FoodAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodAPI.Models.DTO
{
    public class OrderDetailDTO
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }
        public double TotalAmount { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public OrderDetailDTO()
        {

        }

        public OrderDetailDTO(OrderDetail orderDetail)
        {
            Id = orderDetail.Id;
            Price = orderDetail.Price;
            Qty = orderDetail.Qty;
            TotalAmount = orderDetail.TotalAmount;
            OrderId = orderDetail.OrderId;
            ProductId = orderDetail.ProductId;

            Product = orderDetail.Product;
        }
    }
}