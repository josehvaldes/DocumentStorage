using DocuStorage.Common;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DocuStorage.Data.Dapper.Services
{
    public class SqlDapperProvider : ISqlDataProvider
    {
        private readonly IConfiguration _configuration;

        public SqlDapperProvider(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public ISqlDapperWrapper GetConnection()
        {
            return new SqlDapperWrapper(new NpgsqlConnection(_configuration.DatabaseConnection()));
        }

        public ISqlDapperWrapper GetDocumentContentConnection()
        {
            return new SqlDapperWrapper(new NpgsqlConnection(_configuration.DatabaseContentConnection()));
        }
    }
}
