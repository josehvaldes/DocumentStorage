using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuStorage.Common
{
    public class Configuration
    {
        public static string DatabaseConnection() 
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var cs = configuration["ConnectionStrings:postgresql"];

            return cs;
        }

        public static string DatabaseContentConnection()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var cs = configuration["ConnectionStrings:contentdb"];

            return cs;
        }
    }
}
