namespace DocuStorage.Services;

using DocuStorage.Models;
using DocuStorate.Common.Data.Model;
using DocuStorate.Common.Data.Services;



public interface IGroupService
{
    Group Create(GroupRequest request);

    List<Group> GetAll();

    List<Group> GetByUser(int userId);
    void AssignToUser(int userid, int[] groups);
}

