using DocuStorage.Common;
using Npgsql;

namespace DocuStorage.Data.Dapper.Services
{
    public class SqlDapperProvider<T> : ISqlDataProvider<T>
    {
        public SqlDapperProvider() 
        {
        
        }

        public ISqlDapperWrapper GetConnection()
        {
            var connection = new NpgsqlConnection(Configuration.DatabaseConnection());
            return new SqlDapperWrapper(connection);
        }
    }
   
}
