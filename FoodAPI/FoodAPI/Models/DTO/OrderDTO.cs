using FoodAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodAPI.Models.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public double OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public bool IsOrderCompleted { get; set; }
        public int UserId { get; set; }
        public ICollection<OrderDetailDTO> OrderDetails { get; set; }

        public OrderDTO()
        {

        }

        public OrderDTO(Order order)
        {
            FullName = order.FullName;
            Id = order.Id;
            Address = order.Address;
            Phone = order.Phone;
            OrderTotal = order.OrderTotal;
            OrderPlaced = order.OrderPlaced;
            OrderPlaced = order.OrderPlaced;
            IsOrderCompleted = order.IsOrderCompleted;
            UserId = order.UserId;

            //OrderDetails = order.OrderDetails.Select(orderdetail => new OrderDetailDTO(orderdetail)).ToList();
        }
    }
}