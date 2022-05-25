using Microsoft.Extensions.Configuration;

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
            return configuration["AWS:BucketName"];
        }

        public static string AWSAccessKey() 
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return configuration["AWS:AccessKey"];
        }

        public static string AWSSecretKey()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return configuration["AWS:SecretKey"];
        }

        public static string AWSRegion()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return configuration["AWS:Region"];
        }

        public static string RedisConfig()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return configuration["Redis:configuration"];
        }
    }
}
