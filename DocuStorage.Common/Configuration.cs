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
            return configuration["ConnectionStrings:postgresql"];
        }

        public static string DatabaseContentConnection()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return configuration["ConnectionStrings:contentdb"];
        }

        public static string AWSBucketName()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var cs = configuration["AWS:BucketName"];

            return cs;
        }

        public static string AWSAccessKey() 
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var cs = configuration["AWS:AccessKey"];

            return cs;
        }

        public static string AWSSecretKey()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var cs = configuration["AWS:SecretKey"];
            return cs;
        }
        public static string AWSRegion()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var cs = configuration["AWS:Region"];
            return cs;
        }
    }
}
