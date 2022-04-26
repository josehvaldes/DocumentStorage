using DocuStorage.Common;
using Npgsql;

namespace DocuStorage.Data.Dapper.Services
{
    public class SqlDapperProvider : ISqlDataProvider
    {
        public SqlDapperProvider() 
        {
        
        }

        public ISqlDapperWrapper GetConnection()
        {
            return new SqlDapperWrapper(new NpgsqlConnection(Configuration.DatabaseConnection()));
        }

        public ISqlDapperWrapper GetDocumentContentConnection()
        {
            return new SqlDapperWrapper(new NpgsqlConnection(Configuration.DatabaseContentConnection()));
        }
    }
}
