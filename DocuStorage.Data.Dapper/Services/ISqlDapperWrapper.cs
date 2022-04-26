namespace DocuStorage.Data.Dapper.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public interface ISqlDapperWrapper: IDisposable
{
    void Open();
    int Execute(string sql, object? param = null);

    T ExecuteScalar<T>(string sql, object? param = null);

    IEnumerable<T> Query<T>(string sql, object? param = null);

    IEnumerable<dynamic> Query(string sql, object? param = null);

}

