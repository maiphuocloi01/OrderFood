using Newtonsoft.Json;
using OrderFoodApp.Assets.Contains;
using OrderFoodApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OrderFoodApp.Services
{
    public class ProductService
    {
        private static ProductService instance;

        public static ProductService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductService();
                }
                return instance;
            }
            private set => instance = value;
        }

        public static async Task<Product> GetProductById(int ID)
        {

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.GetProductByID, new object[] { ID }));

                    var product = JsonConvert.DeserializeObject<Product>(dataString);

                    return product;
                }
                catch (Exception)
                {
                    return null;
                }
            }
           
        }

        public static async Task<List<ProductByCategory>> GetProductByCategory(int ID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.GetProductByCategoryID, new object[] { ID }));

                    var productList = JsonConvert.DeserializeObject<List<ProductByCategory>>(dataString);

                    return productList;
                }
                catch (Exception)
                {
                    return null;
                }
            }
          
        }

        public static async Task<List<PopularProduct>> GetPopularProducts()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.GetPopularProduct));

                    var productList = JsonConvert.DeserializeObject<List<PopularProduct>>(dataString);

                    return productList;
                }
                catch (Exception)
                {
                    return null;
                }
            }
           
        }
    }
}
