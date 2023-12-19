using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Utility
{
    internal static class DbConnUtil
    {
       private static IConfiguration _iconfiguration;
        static DbConnUtil()
        {
            GetAppSettingFile();
        }
        private static void GetAppSettingFile()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetting.json");
            _iconfiguration= builder.Build();
                
        }
        public static string GetConnectionString()
        {
            return _iconfiguration.GetConnectionString("LocalConnectionString");
        }
    }
}

