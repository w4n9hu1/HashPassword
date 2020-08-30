
using HashPassword.service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
namespace HashPassword

{
    class Program
    {
        private static readonly string SALT = ConfigurationManager.AppSettings["salt"];
        private static readonly string DOMAINS = ConfigurationManager.AppSettings["domains"];
        static void Main(string[] args)
        {
            try
            {
                var lines = new List<string>() { "domain password \r\n" };
                var domainlist = DOMAINS.Split(',').Where(n => !string.IsNullOrEmpty(n));
                if (!domainlist.Any() || string.IsNullOrEmpty(SALT))
                {
                    Console.WriteLine("error config...");
                }
                foreach (var domain in domainlist)
                {
                    var pass = HashPassWordService.HashPassword(domain, SALT);
                    lines.Add(domain + " " + pass);
                }
                HashPassWordService.WriteToFile(lines);
                Console.WriteLine("succes...");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("enter anykey to exit...");
            Console.ReadKey();
        }
    }
}
