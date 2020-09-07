using System;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace Server
{
    public static class Extend
    {
        public static string Md5(this string e)
        {
            var result = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(e))).Replace("-", "");
            return result;
        }

        public static string SaltMd5(this string e, object salt)
        {
            var source = JsonConvert.SerializeObject(new
            {
                e,
                salt
            });

            var result = source.Md5();

            return result;
        }
    }
}