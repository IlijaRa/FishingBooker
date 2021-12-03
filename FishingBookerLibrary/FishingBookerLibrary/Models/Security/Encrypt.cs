using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace FishingBookerLibrary.Models.Security
{
    public class Encrypt
    {
        public static string GetMD5Hash(string password)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] data = Encoding.UTF8.GetBytes(password);
                data = md5.ComputeHash(data);

                StringBuilder sb = new StringBuilder();
                foreach (byte x in data)
                {
                    sb.Append(x.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}

