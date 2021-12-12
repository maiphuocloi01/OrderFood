using FoodAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodAPI.Models.DTO
{
    public class ShoppingCartItemDTO
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }
        public double TotalAmount { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public ShoppingCartItemDTO()
        {

        }

        public ShoppingCartItemDTO(ShoppingCartItem shoppingCartItem)
        {
            Id = shoppingCartItem.Id;
            Price = shoppingCartItem.Price;
            Qty = shoppingCartItem.Qty;
            TotalAmount = shoppingCartItem.TotalAmount;
            ProductId = shoppingCartItem.ProductId;
            CustomerId = shoppingCartItem.CustomerId;
        }
    }
}