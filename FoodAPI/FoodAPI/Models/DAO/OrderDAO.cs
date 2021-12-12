﻿using FoodAPI.Models.DTO;
using FoodAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FoodAPI.Models.DAO
{
    public class OrderDAO
    {
        private static OrderDAO instance;

        public static OrderDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrderDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        FoodAppDbEntities1 db = new FoodAppDbEntities1();
        

        public async Task<int> PlaceOrder(OrderDTO orderDTO)
        {
            Order order = new Order()
            {
                FullName = orderDTO.FullName,
                OrderPlaced = DateTime.Now,
                Address = orderDTO.Address,
                Phone = orderDTO.Phone,
                OrderTotal = orderDTO.OrderTotal,
                IsOrderCompleted = orderDTO.IsOrderCompleted,
                UserId = orderDTO.UserId
            };
            try
            {
                db.Orders.Add(order);
                await db.SaveChangesAsync();
                return order.Id;
            }
            catch (Exception e)
            {
                return -1;
                throw e;
            }
        }

        public async Task<List<OrderDTO>> GetOrdersByUserID(int ID)
        {
            var resultList = (await db.Orders
                .ToListAsync())
                .Select(b => new OrderDTO(b))
                .ToList();
            resultList = resultList.FindAll(b => b.UserId == ID);
            return resultList;
        }
    }
}