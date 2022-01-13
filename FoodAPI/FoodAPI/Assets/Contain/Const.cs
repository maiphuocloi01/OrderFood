using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace FoodAPI.Assets.Contain
{
    public class Const
    {
        public static readonly string Domain = $"http://192.168.1.9/OrderFoodAPI/";
        //public static readonly string Domain = $"http://phuocloi123.somee.com/";

        public static readonly string ProductImagePath = Domain + @"Assets/Images/Product/";
        public static readonly string CategoryImagePath = Domain + @"Assets/Images/Category/";
        public static readonly string AdvertisementImagePath = Domain + @"Assets/Images/Advertisement/";
        public static readonly string UserImagePath = Domain + @"Assets/Images/User/";

        public static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static byte[] BytesFromString(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }
    }
}