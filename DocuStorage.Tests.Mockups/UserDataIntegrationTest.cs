namespace DocuStorage.Tests.Mockups;

using DocuStorage.Data.Dapper.Services;
using DocuStorage.Common.Data.Model;
using DocuStorage.Common.Data.Services;
using NUnit.Framework;

[TestFixture]
public class UserDataIntegrationTest
{

    private IUserDataService _userService;
    private int DefaultUserId = 1;

    [SetUp]
    public void Setup()
    {
        _userService = new UserDataDpService(new SqlDapperProvider());
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
        var user = _userService.Get(DefaultUserId);
        Assert.IsNotNull(user);
    }

    [Test]
    public void Get_ByUser_Users_IsDefault()
    {

        var userdb = _userService.Get(new User() { Username="root", Password="root123" });
        Assert.IsNotNull(userdb);
        Assert.AreEqual(userdb?.Id??0, DefaultUserId);
    }

    [Test]
    public void Create_User_NoExceptions()
    {
        try
        {
            var user = _userService.Create(GetDummyUser());
            _userService.Delete(user.Id);
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }


    [Test]
    public void Update_User_NoExceptions()
    {
        try
        {
            var user = GetDummyUser();
            user.Id = DefaultUserId;
            user.Username = "root";
            user.Password = "321";
            var response = _userService.Update(user);
            Assert.IsTrue(response);
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }

}

