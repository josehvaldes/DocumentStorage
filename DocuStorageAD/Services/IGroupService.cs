using DocuStorage.Common.Data.Model;

namespace DocuStorageAD.Services;

public interface IGroupService
{
    List<Group> GetAll();
}
