using FoodAPI.Models.DTO;
using FoodAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FoodAPI.Models.DAO
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance;

        public static OrderDetailDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrderDetailDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        FoodAppDbEntities1 db = new FoodAppDbEntities1();

        public async Task<List<OrderDetailDTO>> GetOrderDetailByID(int ID)
        {
            var resultList = (await db.OrderDetails
                .ToListAsync())
                .Select(b => new OrderDetailDTO(b))
                .ToList();
            resultList = resultList.FindAll(b => b.OrderId == ID);
            return resultList;
        }
    }
}