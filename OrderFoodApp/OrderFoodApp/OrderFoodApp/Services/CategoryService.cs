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
    public class CategoryService
    {

        public static async Task<List<Category>> GetCategories()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.GetAllCategory));

                    var categoryList = JsonConvert.DeserializeObject<List<Category>>(dataString);

                    return categoryList;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
