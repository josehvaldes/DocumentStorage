using Microsoft.Extensions.Configuration;

namespace DocuStorage.Common
{
    public static class ConfigurationExtensions 
    {
        public static string DatabaseConnection(this IConfiguration configuration) 
        {
            return configuration["ConnectionStrings:postgresql"];
        }

        public static string DatabaseContentConnection(this IConfiguration configuration)
        {
            return configuration["ConnectionStrings:contentdb"];
        }

        public static string AWSBucketName(this IConfiguration configuration)
        {
            return configuration["AWS:BucketName"];
        }

        public static string AWSAccessKey(this IConfiguration configuration)
        {
            return configuration["AWS:AccessKey"];
        }

        public static string AWSSecretKey(this IConfiguration configuration)
        {
            return configuration["AWS:SecretKey"];
        }

        public static string AWSRegion(this IConfiguration configuration)
        {
            return configuration["AWS:Region"];
        }

        public static string RedisConfig(this IConfiguration configuration)
        {
            return configuration["Redis:configuration"];
        }

        public static string AzureStorageConnection(this IConfiguration configuration)
        {
            return configuration["Azure:StorageConnection"];
        }
        public static string AzureContainerName(this IConfiguration configuration)
        {
            return configuration["Azure:ContainerName"];
        }
        public static string AzureTableName(this IConfiguration configuration)
        {
            return configuration["Azure:TableName"];
        }

    }

    public class Configuration
    {


        public static string RedisConfig()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return configuration["Redis:configuration"];
        }

    }
}
