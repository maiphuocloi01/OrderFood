using FoodAPI.Assets.Contain;
using FoodAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodAPI.Models.DTO
{
    public class AdvertisementDTO
    {
        public string Image { get; set; }

        public AdvertisementDTO()
        {

        }

        public AdvertisementDTO(Advertisement advertisement)
        {
            Image = Const.AdvertisementImagePath + advertisement.Image;
        }
    }
}