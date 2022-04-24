namespace DocuStorate.Common.Data.Services;

using DocuStorate.Common.Data.Model;

public interface IDocumentDataService
{
    Document Create(Document document);
    Document? Get(int id);
    List<Document> GetAll();
    List<Document> GetByUserId(int userId);
    List<Document> GetByGroupId(int groupId);
    void AssignToUser(int userId, int [] documents);
    void AssignToGroup(int groupId, int[] documents);
    List<DocumentGroup> GetInGroupsByUser(int userId);
    List<DocumentGroup> GetAllAvailableUser(int userId);
    void Delete(int id);
}

