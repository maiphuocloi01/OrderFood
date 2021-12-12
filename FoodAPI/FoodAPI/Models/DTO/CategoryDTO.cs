using FoodAPI.Assets.Contain;
using FoodAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodAPI.Models.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<ProductDTO> Products { get; set; }

        public CategoryDTO()
        {

        }

        public CategoryDTO(Category category)
        {
            Name = category.Name;
            Id = category.Id;
            ImageUrl = Const.CategoryImagePath + category.ImageUrl;
            Products = category.Products.Select(p => new ProductDTO(p)).ToList();
        }
    }
}