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
                    Preferences.Set("userName", resultCustomer.Name);
                    Preferences.Set("userAvatar", resultCustomer.Avatar);


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

        public static async Task<User> GetUserByID(int ID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.GetUserByID, new object[] { ID }));

                    var customer = JsonConvert.DeserializeObject<User>(dataString);

                    return customer;
                }
                catch (Exception e)
                {
                    return null;
                    throw e;
                }
            }
        }

        public static async Task<HttpResponseMessage> UploadImage(byte[] ImageData, string ImageName)
        {
            if (ImageData != null && !string.IsNullOrEmpty(ImageName))
            {
                using (HttpClient client = new HttpClient())
                {
                    var convertString = Const.ConverToPathWithParameter(Const.UploadImage);
                    var requestContent = new MultipartFormDataContent();
                    var imageContent = new ByteArrayContent(ImageData);
                    imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                    requestContent.Add(imageContent, "image", ImageName);
                    return await client.PostAsync(convertString, requestContent);
                }
            }
            else
            {
                return null;
            }
        }

        public static async Task<bool> UpdateUser(User user)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.UpdateUser);

                    var myContent = JsonConvert.SerializeObject(user);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = client.PostAsync(convertString, byteContent).Result.Content.ReadAsStringAsync().Result;

                    var resultBool = JsonConvert.DeserializeObject<bool>(result);

                    return resultBool;
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
