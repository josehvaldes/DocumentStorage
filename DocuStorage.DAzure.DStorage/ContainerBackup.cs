namespace DocuStorage.DAzure.DStorage;

using DocuStorage.Common.Data.Services;
using Azure.Storage.Blobs;
using DocuStorage.Common;

public class ContainerBackup : IBackup
{
    private readonly string _containerName = Configuration.AzureContainerName();
    private readonly string _connectionString = Configuration.AzureStorageConnection();

    private IDocumentDataService _service;
    
    public ContainerBackup(IDocumentDataService service) 
    {
        _service = service;
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
