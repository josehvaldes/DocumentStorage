using DocuStorage.Common.Data.Model;

namespace DocuStorageAD.Services;

public interface IBackupDocumentsService
{
    Task<IEnumerable<Document>> GetDocuments(string category);
}
