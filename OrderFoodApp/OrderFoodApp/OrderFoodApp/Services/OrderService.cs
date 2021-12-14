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

                    var content = new StringContent(myContent, Encoding.UTF8, "application/json");
                    //var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    //var byteContent = new ByteArrayContent(buffer);

                    //byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = await client.PostAsync(convertString, content);


                    var jsonResult = await result.Content.ReadAsStringAsync();

                    OrderResponse res = new OrderResponse()
                    {
                        orderId = Int32.Parse(jsonResult)
                    };

                    return res;
                }
                catch (Exception e)
                {
                    return null;
                    throw e;
                }
            }
        }

        public static async Task<List<OrderDetail>> GetOrderDetails(int ID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.GetOrderDetailByID, new object[] { ID }));

                    var resultList = JsonConvert.DeserializeObject<List<OrderDetail>>(dataString);

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
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.GetOrdersByUserID, new object[] { ID }));

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
