namespace DocuStorate.Common.Data.Services;

using DocuStorate.Common.Data.Model;

public interface IGroupDataService
{
    Group Create(Group request);

    List<Group> GetAll();
    List<Group> GetByUser(int userId);
    void AssignToUser(int userId, int[] groups);
    void Delete(int id);
}

