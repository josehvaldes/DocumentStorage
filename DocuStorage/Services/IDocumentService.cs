using DocuStorage.Models;
using DocuStorage.Common.Data.Model;
using DocuStorage.Common.Data.Services;


namespace DocuStorage.Services
{
    public interface IDocumentService
    {
        Document Get(int id);
        Document Create(DocumentRequest document);
        List<Document> GetAll();
        List<Document> GetByUserId(int userId);
        List<Document> GetByGroupId(int groupId);
        List<DocumentGroup> GetInGroupsByUser(int userId);
        List<DocumentGroup> GetAllAvailableUser(int userId);

        void AssignToUser(int userId, int[] documents);
        void AssignToGroup(int groupId, int[] documents);
        void Delete(int id);

        Task<bool> Backup(int id);
    }
}
