using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lilac.LilacCore.Utility
{
    public static class SHA1Helper
    {
        /// <summary>
        /// 对字符串进行SHA1散列
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string SHA1(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            byte[] data = Encoding.ASCII.GetBytes(input);
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                byte[] result = sha1.ComputeHash(data);
                StringBuilder sb = new StringBuilder(result.Length * 2);
                foreach (byte val in result)
                {
                    sb.AppendFormat(val.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
