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

    public bool Backup(int documentId)
    {
        var document = _service.Get(documentId);
        if (document != null)
        {
            BlobClient blobClient = new BlobClient(
                    connectionString: _connectionString,
                    blobContainerName: _containerName,
                    blobName: $"{document.Id}_{document.Name}");

            if (!blobClient.Exists())
            {
                using var ms = new MemoryStream(document.Content);
                blobClient.Upload(ms);
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
}
