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

        FoodAppDbEntities db = new FoodAppDbEntities();

        public async Task<List<OrderDTO>> GetOrderDetailByID(int ID)
        {
            var resultList = (await db.Orders
                //.Where(order => order.Id == ID)
                //.Include(co => co.OrderDetails.Select(emp => new OrderDetailDTO(emp)))
                .ToListAsync())
                .Select(b => new OrderDTO(b))
                .ToList();
            resultList = resultList.FindAll(b => b.Id == ID);

            return resultList;
            //var resultList = (await db.OrderDetails
            //    .ToListAsync())
            //    .Select(b => new OrderDetailDTO(b))
            //    .ToList();
            //resultList = resultList.FindAll(b => b.OrderId == ID);
            //return resultList;
        }
    }
}