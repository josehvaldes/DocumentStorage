namespace DocuStorage.Tests.Mockups;

using DocuStorage.Data.Dapper.Services;
using DocuStorate.Common.Data.Model;
using DocuStorate.Common.Data.Services;
using NUnit.Framework;
using Moq;

[TestFixture]
public class GroupDataMkTest
{
    private IGroupDataService _groupDataService;
    private int _defaultGroupId = 3;
    private int DefaultUserId = 1;
    [SetUp]
    public void Setup()
    {
        _groupDataService = new GroupDataDpService();
        
    }

    private Group GetDummyGroup()
    {
        Group group = new Group()
        {
            Name = "Dapper Group",
        };
        return group;
    }

    [Test]
    public void Create_Group_NotEmpty()
    {
        try
        {
            var response = _groupDataService.Create(GetDummyGroup());
            Assert.IsNotNull(response);
            Assert.Greater(response.Id, 0);
            _groupDataService.Delete(response.Id);
        }
        catch (Exception) 
        {
            Assert.Fail();
        }
        
    }

    [Test]
    public void Delete_Group_NoException()
    {
        try 
        {
            _groupDataService.Delete(_defaultGroupId);
        }
        catch(Exception)
        {
            Assert.Fail();
        }
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
        var list = _groupDataService.GetByUser(1);
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

