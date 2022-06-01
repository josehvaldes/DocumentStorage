using DocuStorage.Common.Data.Model;

namespace DocuStorage.Services;

public interface IBackupDocumentsService
{
    Task<IEnumerable<Document>> GetDocuments(string category);
}
