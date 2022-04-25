namespace DocuStorage.Data.Dapper.Services;

using global::Dapper;
using Npgsql;

public class SqlDapperWrapper : ISqlDapperWrapper
{
    public NpgsqlConnection _connection;


    public SqlDapperWrapper() 
    {
        _connection = new NpgsqlConnection();
    }

    public SqlDapperWrapper(NpgsqlConnection connection)
    {
        _connection = connection;
    }

    public void Open()
    {
        _connection.Open();
    }

    public int Execute(string sql, object? param)
    {
        return _connection.Execute(sql, param);
    }


    public T ExecuteScalar<T>(string sql, object? param =null)
    {
        return _connection.ExecuteScalar<T>(sql, param);
    }

    public IEnumerable<T> Query<T>(string sql, object? param = null) 
    {
        return _connection.Query<T>("select * from get_groups()", param);
    }

    #region IDisposable Members 
    public void Dispose()
    {
        _connection?.Close();
    }

    #endregion
}

