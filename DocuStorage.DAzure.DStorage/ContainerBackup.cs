namespace DocuStorage.DAzure.DStorage;

using DocuStorage.Common.Data.Services;
using Azure.Storage.Blobs;
using DocuStorage.Common;
using Microsoft.Extensions.Configuration;

public class ContainerBackup : IBackup
{
    private readonly IConfiguration _configuration;
    private readonly string _containerName ;
    private readonly string _connectionString;

    private IDocumentDataService _service;
    private IMirror<DocumentEntity> _mirror;

    public ContainerBackup(IDocumentDataService service, IConfiguration configuration, IMirror<DocumentEntity> mirror) 
    {
        _service = service;
        _configuration = configuration;
        _mirror = mirror;

        _containerName = _configuration.AzureContainerName();
        _connectionString = _configuration.AzureStorageConnection();
}

    public async Task<bool> Backup(int documentId)
    {
        var document = _service.Get(documentId);
        if (document != null)
        {
            BlobClient blobClient = new BlobClient(
                    connectionString: _connectionString,
                    blobContainerName: _containerName,
                    blobName: $"{document.Id}_{document.Name}");

            var metadata = new Dictionary<string, string>();
            
            metadata["description"] = document.Description;
            metadata["created_on"] = document.Created_On.ToString();

            if (!blobClient.Exists())
            {
                using var ms = new MemoryStream(document.Content);
                await blobClient.UploadAsync(ms);
                await blobClient.SetMetadataAsync(metadata); // execute sync to ensure it is done
                await _mirror.AddAsync(Converter.Convert(document));
                return true;
            }
            else 
            {
                return false;
            }
        }
        else 
        {
            throw new Exception("No document found to backup");
        }
    }

    public async Task<bool> Delete(int documentId)
    {
        var document = _service.Get(documentId);
        if (document != null)
        {
            BlobClient blobClient = new BlobClient(
                    connectionString: _connectionString,
                    blobContainerName: _containerName,
                    blobName: $"{document.Id}_{document.Name}");

            if (blobClient.Exists())
            {
                var response = await blobClient.DeleteAsync();
                return response.Status == 202 ;
            }
            else 
            {
                return false;
            }
        }

        // no document found
        return true;
    }
}
