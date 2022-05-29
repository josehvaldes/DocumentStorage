using DocuStorage.Common.Data.Model;
using DocuStorageAD.Models;

namespace DocuStorageAD.Services;

public interface IDocumentService
{

    Document Get(int id);
    Document Create(DocumentRequest document);
    List<Document> GetAll();
}

