namespace DocuStorage.DAzure.DStorage;

using Azure.Data.Tables;
using DocuStorage.Common;
using DocuStorage.Common.Data.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DocumentTableMirror : IMirror<DocumentEntity>
{
    private readonly string _connectionString;
    private readonly string _tableName;
    private readonly IConfiguration _configuration;

    public DocumentTableMirror(IConfiguration configuration) 
    {
        _configuration = configuration;
        _connectionString = _configuration.AzureStorageConnection();
        _tableName = _configuration.AzureTableName();
    }

    private async Task<TableClient> GetTableClient() 
    {
        var serviceClient = new TableServiceClient(_connectionString);
        var tableClient = serviceClient.GetTableClient(_tableName);
        await tableClient.CreateIfNotExistsAsync();
        return tableClient;
    }

    public async Task<DocumentEntity> AddAsync(DocumentEntity entity)
    {
        var tableClient = await GetTableClient();
        await tableClient.UpsertEntityAsync(entity);
        return entity;
    }

    public async Task DeleteAsync(DocumentEntity entity)
    {
        var tableClient = await GetTableClient();
        var response = await tableClient.DeleteEntityAsync(entity.PartitionKey, entity.RowKey);
    }

    public async Task<DocumentEntity> GetAsync(string partitionKey, string rowKey)
    {
        var tableClient = await GetTableClient();
        return await tableClient.GetEntityAsync<DocumentEntity>(partitionKey, rowKey);
    }
}
