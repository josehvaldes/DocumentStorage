namespace DocuStore.Tests;
using NUnit.Framework;
using DocuStorage.Data.Services;
using DocuStorage.Common.Data.Model;
using DocuStorage.Common.Data.Services;
using Microsoft.Extensions.Configuration;

/// <summary>
/// Unit for UserDataService class
/// </summary>
[TestFixture]
public class UserDataTests
{
 
    private IUserDataService _userService;
    private User _dummyUser;
    private IConfiguration _configuration;

    [SetUp]
    public void Setup() 
    {
        _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _userService = new UserDataService(_configuration);
        _dummyUser = _userService.Create(GetDummyUser());

    }

    [TearDown]
    public void TearDown() 
    {
        _userService.Delete(_dummyUser.Id);
    }

    private User GetDummyUser()
    {
        var user = new User()
        {
            Username = "testuser",
            Password = "123",
            Role = 1
        };
        return user;
    }

    [Test]
    public void GetALL_Users_NotEmpty() 
    {
        var list = _userService.GetAll();
        Assert.IsNotEmpty(list);
    }

    [Test]
    public void Get_Users_IsDefault()
    {        
        var user = _userService.Get(_dummyUser.Id);
        Assert.IsNotNull(user);
    }

    [Test]
    public void Get_ByUser_Users_IsDefault()
    {

        var userdb = _userService.Get(_dummyUser);
        Assert.IsNotNull(userdb);
        Assert.Greater(userdb.Id,0);
    }

    [Test]
    public void Create_User_IDIsNotZero() 
    {
        var userdb = _userService.Create(new User() { Username = "newuser", Password = "test123", Role = 2 });
        Assert.Greater(userdb.Id,0);
        
        _userService.Delete(userdb.Id);
    }


    [Test]
    public void Update_User_NoExceptions()
    {
        try {
            var newUsername = "NewUsername";
            _dummyUser.Username = newUsername;
            var response = _userService.Update(_dummyUser);
            Assert.IsTrue(response);
        }
        catch (Exception e) 
        {
            Assert.Fail(e.Message);
        }  
    }


    [Test]
    public void Delete_User_NoExceptions()
    {
        try 
        {
            _userService.Delete(_dummyUser.Id);
            var user = _userService.Get(_dummyUser.Id);
            Assert.IsNull(user);
        } 
        catch (Exception e) 
        {
            Assert.Fail(e.Message);
        }            
    }
}
