using FoodAPI.Assets.Contain;
using FoodAPI.Models.EF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodAPI.Models.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public bool IsPopularProduct { get; set; }
        public int CategoryId { get; set; }
        [JsonIgnore]
        public ICollection<OrderDetailDTO> OrderDetails { get; set; }

        [JsonIgnore]
        public ICollection<ShoppingCartItemDTO> ShoppingCartItems { get; set; }
        public ProductDTO()
        {

        }

        public ProductDTO(Product product)
        {
            Name = product.Name;
            Id = product.Id;
            ImageUrl = Const.ProductImagePath + product.ImageUrl;
            Detail = product.Detail;
            Price = product.Price;
            IsPopularProduct = product.IsPopularProduct;
            CategoryId = product.CategoryId;

            OrderDetails = product.OrderDetails.Select(o => new OrderDetailDTO(o)).ToList();
            ShoppingCartItems = product.ShoppingCartItems.Select(s => new ShoppingCartItemDTO(s)).ToList();
        }
    }
}