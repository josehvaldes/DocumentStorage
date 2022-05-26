namespace DocuStorage.Services;

using DocuStorage.Models;
using DocuStorage.Common.Data.Model;
using DocuStorage.Common.Data.Services;


public class DocumentService : IDocumentService
{
    private IDocumentDataService _documentService;
    private IBackup _backup;

    public DocumentService(IDocumentDataService documentService, IBackup backup)
    {
        _documentService = documentService;
        _backup = backup;
    }

    public void AssignToGroup(int groupId, int[] documents)
    {
        _documentService.AssignToGroup(groupId, documents);
    }

    public void AssignToUser(int userId, int[] documents)
    {
        _documentService.AssignToUser(userId, documents);
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

    public List<Document> GetByUserId(int userId)
    {
        return _documentService.GetByUserId(userId);
    }

    public List<Document> GetByGroupId(int groupId)
    {
        return _documentService.GetByGroupId(groupId);
    }

    public List<DocumentGroup> GetInGroupsByUser(int userId)
    {
        return _documentService.GetInGroupsByUser(userId);
    }

    public List<DocumentGroup> GetAllAvailableUser(int userId)
    {
        return _documentService.GetAllAvailableUser(userId);
    }

    public void Delete(int id)
    {
        _documentService.Delete(id);
    }

    public bool Backup(int id)
    {
        return _backup.Backup(id);
    }
}

