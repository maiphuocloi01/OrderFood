using FoodAPI.Assets.Contain;
using FoodAPI.Models.EF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodAPI.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Avatar { get; set; }
        public ICollection<OrderDTO> Orders { get; set; }

        public UserDTO()
        {

        }

        public UserDTO(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            Password = user.Password;
            Role = user.Role;
            Avatar = Const.UserImagePath + user.Avatar;

            Orders = user.Orders.Select(order => new OrderDTO(order)).ToList();
        }
    }
}