using OrderFoodApp.Assets.Contains;
using OrderFoodApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Xamarin.Essentials;

namespace OrderFoodApp.Services
{
    public class UserService
    {
        private static UserService instance;

        public static UserService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserService();
                }
                return instance;
            }
            private set => instance = value;
        }

        public static async Task<bool> Login(string userName, string passWord)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.LoginPath);

                    Login login = new Login() { Email = userName, Password = passWord };

                    var myContent = JsonConvert.SerializeObject(login);

                    var content = new StringContent(myContent, Encoding.UTF8, "application/json");

                    //var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    //var byteContent = new ByteArrayContent(buffer);

                    //byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = await client.PostAsync(convertString, content); //.Result.Content.ReadAsStringAsync().Result;
                    if (!result.IsSuccessStatusCode) return false;
                    var jsonResult = await result.Content.ReadAsStringAsync();
                    var resultCustomer = JsonConvert.DeserializeObject<Login>(jsonResult);

                    //var userId = resultCustomer.Email;
                    Preferences.Set("userId", resultCustomer.Id);

                    
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                    throw e;
                }
            }
        }

        public static async Task<bool> Register(Register register)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.RegisterPath);

                    var myContent = JsonConvert.SerializeObject(register);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = client.PostAsync(convertString, byteContent).Result.Content.ReadAsStringAsync().Result;

                    var resultID = JsonConvert.DeserializeObject<bool>(result);

                    return resultID;
                }
                catch (Exception e)
                {
                    return false;
                    throw e;
                }
            }
        }
    }
}
