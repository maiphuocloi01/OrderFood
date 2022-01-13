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

        public static async Task<List<Product>> GetAllProduct()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.GetAllProduct);
                    var dataString = await client.GetStringAsync(convertString);

                    var ProductList = JsonConvert.DeserializeObject<List<Product>>(dataString);

                    //ProductList.Sort((p1, p2) => p2.DiscountPrice.CompareTo(p1.DiscountPrice));

                    return ProductList;
                }
                catch (Exception e)
                {
                    return null;
                    throw e;
                }
            }
        }

        public static async Task<List<Advertisement>> GetAllAdvertisement()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.GetAllAdvertisement);
                    var dataString = await client.GetStringAsync(convertString);

                    var AdvertisementList = JsonConvert.DeserializeObject<List<Advertisement>>(dataString);

                    return AdvertisementList;
                }
                catch (Exception e)
                {
                    return null;
                    throw e;
                }
            }
        }

        public static async Task<List<Product>> GetProductBySearchText(string searchText)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.GetAllProduct);
                    var dataString = await client.GetStringAsync(convertString);

                    var ProductList = JsonConvert.DeserializeObject<List<Product>>(dataString);

                    ProductList = ProductList.FindAll(p => Const.ConvertToUnsign(p.name).IndexOf(searchText, 0, StringComparison.CurrentCultureIgnoreCase) != -1 ||
                    p.name.IndexOf(searchText, 0, StringComparison.CurrentCultureIgnoreCase) != -1);

                    return ProductList;
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
