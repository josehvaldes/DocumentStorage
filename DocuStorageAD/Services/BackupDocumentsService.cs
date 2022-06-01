using DocuStorage.Common.Data.Model;
using DocuStorage.DAzure.DStorage;

namespace DocuStorageAD.Services;

public class BackupDocumentsService : IBackupDocumentsService
{
    private IMirror<DocumentEntity> _backupMirror;


    public BackupDocumentsService(IMirror<DocumentEntity> backupMirror) 
    {
        _backupMirror = backupMirror;
    }

    public async Task<IEnumerable<Document>> GetDocuments(string category)
    {
        var query = $"PartitionKey eq '{category}'";
        var list = await _backupMirror.QueryAsync(query);
        var documentList = list.Select(x => Converter.Convert(x));
        return documentList;
    }
}
