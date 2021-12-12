using Newtonsoft.Json;
using OrderFoodApp.Assets.Contains;
using OrderFoodApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OrderFoodApp.Services
{
    public class OrderService
    {
        private static OrderService instance;

        public static OrderService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrderService();
                }
                return instance;
            }
            private set => instance = value;
        }

        public static async Task<OrderResponse> PlaceOrder(Order order)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.PlaceOrder);

                    var myContent = JsonConvert.SerializeObject(order);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = client.PostAsync(convertString, byteContent).Result.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<OrderResponse>(result);
                }
                catch (Exception e)
                {
                    return null;
                    throw e;
                }
            }
        }

        public static async Task<List<Order>> GetOrderDetails(int ID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.GetOrderDetailByID, new object[] { ID }));

                    var resultList = JsonConvert.DeserializeObject<List<Order>>(dataString);

                    return resultList;
                }
                catch (Exception e)
                {
                    return null;
                    throw e;
                }
            }
        }

        public static async Task<List<OrderByUser>> GetOrdersByUser(int ID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.GetOrderDetailByID, new object[] { ID }));

                    var resultList = JsonConvert.DeserializeObject<List<OrderByUser>>(dataString);

                    return resultList;
                }
                catch (Exception e)
                {
                    return null;
                    throw e;
                }
            }
        }
    }
}
