using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OrderFoodApp.Assets.Contains
{
    public class Const
    {
        public static readonly string Domain = $"http://192.168.1.9/OrderFoodAPI/";

        public static readonly string LoginPath = Domain + @"Api/UserController/Login";
        public static readonly string RegisterPath = Domain + @"Api/UserController/Register";
        public static readonly string UpdateUser = Domain + @"Api/UserController/UpdateUser";
        public static readonly string UploadImage = Domain + @"Api/UserController/UploadImage";
        public static readonly string GetUserByID = Domain + @"Api/UserController/GetUserByID/{ID}";

        public static readonly string GetAllCategory = Domain + @"Api/CategoryController/GetAllCategory";
        
        public static readonly string PlaceOrder = Domain + @"Api/OrderController/PlaceOrder";
        public static readonly string GetOrderDetailByID = Domain + @"Api/OrderController/GetOrderDetailByID/{ID}";
        public static readonly string GetOrdersByUserID = Domain + @"Api/OrderController/GetOrdersByUserID/{ID}";

        public static readonly string GetProductByID = Domain + @"Api/ProductController/GetProductByID/{ID}";
        public static readonly string GetProductByCategoryID = Domain + @"Api/ProductController/GetProductByCategoryID/{ID}";
        public static readonly string GetPopularProduct = Domain + @"Api/ProductController/GetPopularProduct";
        public static readonly string GetAllAdvertisement = Domain + @"Api/AdvertisementController/GetAllAdvertisement";
        public static readonly string GetAllProduct = Domain + @"Api/ProductController/GetAllProduct";

        public static readonly string SubTotal = Domain + @"Api/ShoppingCartItemController/SubTotal/{ID}";
        public static readonly string TotalItems = Domain + @"Api/ShoppingCartItemController/TotalItems/{ID}";
        public static readonly string GetAllShoppingCartItems = Domain + @"Api/ShoppingCartItemController/GetAllShoppingCartItems/{ID}";
        public static readonly string DeleteCartItem = Domain + @"Api/ShoppingCartItemController/DeleteCartItem/{ID}";
        public static readonly string AddShoppingCartItems = Domain + @"Api/ShoppingCartItemController/AddShoppingCartItems";
        public static readonly string UpdateQuantity = Domain + @"Api/ShoppingCartItemController/UpdateQuantity/{ID}/{quantity}";
        public static readonly string DeleteCartItemByID = Domain + @"Api/ShoppingCartItemController/DeleteCartItemByID/{ID}";


        public static string ConverToPathWithParameter(string path, object[] param = null)
        {
            if (param == null)
                return path;

            foreach (var item in param)
            {
                var startIndex = path.IndexOf("{");
                var endIndex = path.IndexOf("}");
                var oldString = path.Substring(startIndex, endIndex - startIndex + 1);
                path = path.Replace(oldString, item.ToString());
            }
            return path;
        }

        public static string ConvertToUnsign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
