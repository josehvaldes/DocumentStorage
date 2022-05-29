using DocuStorage.Common.Data.Model;
using DocuStorage.Common.Data.Services;
using DocuStorageAD.Models;

namespace DocuStorageAD.Services;

public class DocumentService : IDocumentService
{
    private IDocumentDataService _documentService;

    public DocumentService(IDocumentDataService documentService)
    {
        _documentService = documentService;
    }

    public Document Create(DocumentRequest filerequest)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            filerequest.FormFile.CopyTo(stream);
            var array = stream.ToArray();

            var document = _documentService.Create(new Document()
            {
                Name = filerequest.Name,
                Category = filerequest.Category,
                Description = filerequest.Description,
                Content = array
            });

            return document;
        }
    }

    public Document Get(int id)
    {
        return _documentService.Get(id);
    }

    public List<Document> GetAll()
    {
        return _documentService.GetAll();
    }
}
