using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HashMe
{
    public class HashMan
    {
        public static string GetMD5Hash(string clearText)
        {
            MD5CryptoServiceProvider MD5Provider = new MD5CryptoServiceProvider();
            var hashedValue = MD5Provider.ComputeHash(Encoding.Default.GetBytes(clearText));
            StringBuilder SB = new StringBuilder();

            for (int i = 0; i < hashedValue.Length; i++)
            {
                SB.Append(hashedValue[i].ToString("x2"));
            }

            return SB.ToString();
        }
    }
}
