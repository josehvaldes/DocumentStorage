namespace DocuStorage.DAzure.DStorage;

using Azure;
using Azure.Data.Tables;
using System;

public class DocumentEntity : ITableEntity
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }

    public string Created_On { get; set; }

    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}
