﻿using System;
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
        public static readonly string Domain = $"http://192.168.1.8/OrderFoodAPI/";

        public static readonly string ProductImagePath = Domain + @"Assets/Images/Product/";
        public static readonly string CategoryImagePath = Domain + @"Assets/Images/Category/";
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

        public static async Task<bool> VerifyEmail(string email)
        {
            Task<bool> task = new Task<bool>(new Func<bool>(() =>
            {
                TcpClient tClient = new TcpClient("gmail-smtp-in.l.google.com", 25);
                string CRLF = "\r\n";
                byte[] dataBuffer;
                string ResponseString;
                NetworkStream netStream = tClient.GetStream();
                StreamReader reader = new StreamReader(netStream);
                ResponseString = reader.ReadLine();
                /* Perform HELO to SMTP Server and get Response */
                dataBuffer = BytesFromString("HELO KirtanHere" + CRLF);
                netStream.Write(dataBuffer, 0, dataBuffer.Length);
                ResponseString = reader.ReadLine();
                dataBuffer = BytesFromString("MAIL FROM:<YourGmailIDHere@gmail.com>" + CRLF);
                netStream.Write(dataBuffer, 0, dataBuffer.Length);
                ResponseString = reader.ReadLine();
                /* Read Response of the RCPT TO Message to know from google if it exist or not */
                dataBuffer = BytesFromString("RCPT TO:<" + email + ">" + CRLF);
                netStream.Write(dataBuffer, 0, dataBuffer.Length);
                ResponseString = reader.ReadLine();
                /* QUITE CONNECTION */
                dataBuffer = BytesFromString("QUITE" + CRLF);
                netStream.Write(dataBuffer, 0, dataBuffer.Length);
                tClient.Close();

                return ResponseString.Contains("OK");
            }));

            task.Start();

            return await task;
        }

        public static byte[] BytesFromString(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }
    }
}