namespace DocuStorage.Tests.Mockups;

using DocuStorage.Data.Dapper.Services;
using DocuStorage.Common.Data.Model;
using NUnit.Framework;
using System;
using Microsoft.Extensions.Configuration;

[TestFixture]
public class GroupDataIntegrationTest
{
    private GroupDataDpService _groupDataService;
    private int DefaultUserId = 1;
    private IConfiguration _configuration;

    [SetUp]
    public void Setup() 
    {
        _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _groupDataService = new GroupDataDpService(new SqlDapperProvider(_configuration));
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
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }

    }

    [Test]
    public void Delete_Group_NoException()
    {
        try
        {
            var response = _groupDataService.Create(GetDummyGroup());
            _groupDataService.Delete(response.Id);
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }

    [Test]
    public void Get_Groups_IsNotEmpty()
    {
        var list = _groupDataService.GetAll();
        Assert.IsNotEmpty(list);
    }

    
    public void GetGroupByUser_IsNotEmpty()
    {
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
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }
}

