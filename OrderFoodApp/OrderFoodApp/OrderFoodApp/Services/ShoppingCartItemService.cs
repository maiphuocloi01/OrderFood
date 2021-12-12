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
    public class ShoppingCartItemService
    {
        private static ShoppingCartItemService instance;

        public static ShoppingCartItemService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShoppingCartItemService();
                }
                return instance;
            }
            private set => instance = value;
        }

        public static async Task<bool> AddItemsInCart(AddToCart addToCart)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.AddShoppingCartItems);

                    var myContent = JsonConvert.SerializeObject(addToCart);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = client.PostAsync(convertString, byteContent).Result.Content.ReadAsStringAsync().Result;

                    var resultAdd = JsonConvert.DeserializeObject<bool>(result);

                    return resultAdd;
                }
                catch (Exception e)
                {
                    return false;
                    throw e;
                }
            }
        }

        public static async Task<CartSubTotal> GetCartSubTotal(int ID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.SubTotal, new object[] { ID }));

                    var subTotal = JsonConvert.DeserializeObject<CartSubTotal>(dataString);

                    return subTotal;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            
        }

        public static async Task<List<ShoppingCartItem>> GetShoppingCartItems(int ID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.GetAllShoppingCartItems, new object[] { ID }));

                    var cartList = JsonConvert.DeserializeObject<List<ShoppingCartItem>>(dataString);

                    return cartList;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            
        }

        public static async Task<TotalCartItem> GetTotalCartItems(int ID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.TotalItems, new object[] { ID }));

                    var totalCart = JsonConvert.DeserializeObject<TotalCartItem>(dataString);

                    return totalCart;
                }
                catch (Exception)
                {
                    return null;
                }
            }
           
        }

        public static async Task<bool> ClearShoppingCart(int ID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.DeleteAsync(Const.ConverToPathWithParameter(Const.DeleteCartItem, new object[] { ID }));

                    //var totalCart = JsonConvert.DeserializeObject<TotalCartItem>(dataString);

                    //return totalCart;

                    if (!dataString.IsSuccessStatusCode) return false;
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
