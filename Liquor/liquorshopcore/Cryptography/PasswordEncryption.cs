using System;
using System.Security.Cryptography;
using System.Text;

namespace EvanDangol.Cryptography
{
    public static class CryptoEngine
    {
        public static string Encrypt(this string input, string key=null)
        {
            if(key==null)
            {
                key = "sblw-3hn8-sqoy19";
            }
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string Decrypt(this string input, string key=null)
        {
            if(key==null)
            {
                key = "sblw-3hn8-sqoy19";
            }
            //string dummyData = input.Trim().Replace(" ", "+");
            //if (dummyData.Length % 4 > 0)
            //    dummyData = dummyData.PadRight(dummyData.Length + 4 - dummyData.Length % 4, '=');
            //byte[] inputArray = Convert.FromBase64String(dummyData);
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear(); 
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    