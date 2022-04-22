using DocuStorage.Models;
using DocuStorate.Data.Model;

namespace DocuStorage.Services
{
    public interface IGroupService
    {
        Group Create(GroupRequest request);

        List<Group> GetAll();

        List<Group> GetByUser(int userId);
        void AssignToUser(int userid, int[] groups);
    }
}
