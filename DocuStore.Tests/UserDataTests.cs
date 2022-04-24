namespace DocuStore.Tests;
using NUnit.Framework;
using DocuStorate.Data.Services;
using DocuStorate.Common.Data.Model;
using DocuStorate.Common.Data.Services;

/// <summary>
/// Unit for UserDataService class
/// </summary>
[TestFixture]
public class UserDataTests
{
    //private const int DefaultUserId = 1;

    private IUserDataService _userService;
    private User _dummyUser;
    [SetUp]
    public void Setup() 
    {
        _userService = new UserDataService();
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
            _userService.Update(_dummyUser);
            var user = _userService.Get(_dummyUser.Id);
            Assert.AreEqual(user.Username, newUsername);
        }
        catch (Exception ) 
        {
            Assert.Fail();
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
        catch (Exception) 
        {
            Assert.Fail();
        }            
    }
}
