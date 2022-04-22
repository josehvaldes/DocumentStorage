namespace DocuStore.Tests;
using DocuStorate.Data.Model;
using DocuStorate.Data.Services;
using NUnit.Framework;

/// <summary>
/// Unit for GroupDataService class
/// </summary>
[TestFixture]
public class GroupDataTests
{
    private const int DefaultGroupId = 1;
    private const int DefaultUserId = 1;

    private IGroupDataService _groupDataService;

    [SetUp]
    public void Setup() 
    {
        _groupDataService = new GroupDataService();
    }

    private Group GetDummyGroup() 
    {
        Group group = new Group()
        {
            Name = "Demo Group",
        };
        return group;
    }


    [Test]
    public void Create_Group_NotEmpty() 
    {
        var response = _groupDataService.Create(GetDummyGroup());
        Assert.IsNotNull(response);
        Assert.Greater(response.Id, 0);
        
    }
        
    [Test]
    public void Get_Groups_IsNotEmpty()
    {
        var list = _groupDataService.GetAll();
        Assert.IsNotEmpty(list);
    }

    [Test]
    public void GetGroupByUser_IsNotEmpty()
    {
        var group = _groupDataService.Create(GetDummyGroup());
        int[] groups = new int[] { group.Id };
        _groupDataService.AssignToUser(DefaultUserId, groups);

        var list = _groupDataService.GetByUser(DefaultUserId);
        Assert.IsNotEmpty(list);
    }

    [Test]
    public void AssignToUser_NoExceptions()
    {
        try 
        {
            var group = _groupDataService.Create(GetDummyGroup());
            int[] groups = new int[] { group.Id };
            _groupDataService.AssignToUser(DefaultUserId, groups);

            _groupDataService.Delete(group.Id);
        }
        catch (Exception) 
        {
            Assert.Fail();
        }
    }
}

