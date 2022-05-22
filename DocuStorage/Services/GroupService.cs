namespace DocuStorage.Services;

using DocuStorage.Models;
using DocuStorage.Common.Data.Model;
using DocuStorage.Common.Data.Services;


public class GroupService : IGroupService
{
    private IGroupDataService _groupDataService;
    public GroupService(IGroupDataService groupDataService) 
    {
        _groupDataService = groupDataService;
    }

    public void AssignToUser(int userId, int[] groups)
    {
        _groupDataService.AssignToUser(userId, groups);
    }

    public Group Create(GroupRequest request)
    {
        var group = _groupDataService.Create(new Group() { 
            Name = request.Name
        });

        return group; ;
    }

    public List<Group> GetAll()
    {
        return _groupDataService.GetAll();
    }

    public List<Group> GetByUser(int userId)
    {
        return _groupDataService.GetByUser(userId);
    }
}

