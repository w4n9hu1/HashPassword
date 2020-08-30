using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace HashPassword.service

{
    public class HashPassWordService
    {
        /// <summary>
        /// generate password
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string HashPassword(string domain, string salt)
        {
            var source = domain + salt;
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }
            MD5 md5 = MD5.Create();
            byte[] byteOld = Encoding.UTF8.GetBytes(source);
            byte[] byteNew = md5.ComputeHash(byteOld);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in byteNew)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString().Substring(8, 16);
        }

        /// <summary>
        /// write to test file
        /// </summary>
        /// <param name="lines"></param>
        public static void WriteToFile(List<string> lines)
        {
            lines.Add($"\r\ncreated by hashpass {DateTime.Now}");
            var path = Environment.CurrentDirectory + $@"\PassBook_{DateTime.Now:yyMMddhhmmss}.txt";
            File.WriteAllLines(path, lines);
        }
    }
}
