using DocuStorage.Common.Data.Model;
using DocuStorage.Common.Data.Services;

namespace DocuStorageAD.Services;

public class GroupService : IGroupService
{
    private IGroupDataService _groupDataService;
    public GroupService(IGroupDataService groupDataService)
    {
        _groupDataService = groupDataService;
    }

    public List<Group> GetAll()
    {
        return _groupDataService.GetAll();
    }
}
