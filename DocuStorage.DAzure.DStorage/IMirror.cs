namespace DocuStorage.DAzure.DStorage;

using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IMirror<T> where T : ITableEntity
{

    Task<T> AddAsync(T entity);
    Task DeleteAsync(T entity);

    Task<T> GetAsync(string partitionKey, string rowKey);

}
