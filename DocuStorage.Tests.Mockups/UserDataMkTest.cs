namespace DocuStorage.Tests.Mockups;

using DocuStorage.Data.Dapper.Services;
using DocuStorate.Common.Data.Model;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;



[TestFixture]
public class UserDataMkTest
{

    public void GetALL_Users_NotEmpty()
    {
        
        var dpWrapper = new Mock<ISqlDapperWrapper>();
        dpWrapper.Setup(x => x.Query<User>(It.IsAny<string>(), It.IsAny<object>())).
            Returns(new List<User>() { new User() { Id = 1, Username = "Moq", Password="pwd", Role=1 } });

        var sqlprovider = new Mock<ISqlDataProvider>();
        sqlprovider.Setup(p => p.GetConnection()).Returns(dpWrapper.Object);

        var userService = new UserDataDpService(sqlprovider.Object);
        var list = userService.GetAll();
        Assert.IsNotEmpty(list);
    }

    [Test]
    public void Get_Users_IsDefault()
    {
        var dpWrapper = new Mock<ISqlDapperWrapper>();
        dpWrapper.Setup(x => x.Query<User>(It.IsAny<string>(), It.IsAny<object>())).
            Returns(new List<User>() { new User() { Id = 1, Username = "Moq", Password = "pwd", Role = 1 } });


        var sqlprovider = new Mock<ISqlDataProvider>();
        sqlprovider.Setup(p => p.GetConnection()).Returns(dpWrapper.Object);

        var userService = new UserDataDpService(sqlprovider.Object);
        var user = userService.Get(1);
        Assert.IsNotNull(user);
    }

    [Test]
    public void Get_ByUser_Users_IsDefault()
    {
        var dpWrapper = new Mock<ISqlDapperWrapper>();
        dpWrapper.Setup(x => x.Query<User>(It.IsAny<string>(), It.IsAny<object>())).
            Returns(new List<User>() { new User() { Id = 1, Username = "Moq", Password = "root123", Role = 1 } });


        var sqlprovider = new Mock<ISqlDataProvider>();
        sqlprovider.Setup(p => p.GetConnection()).Returns(dpWrapper.Object);

        
        var userService = new UserDataDpService(sqlprovider.Object);
        var userdb = userService.Get(new User() { Username = "Moq", Password = "root123" });

        Assert.IsNotNull(userdb);
        Assert.AreEqual(userdb?.Id ?? 0, 1);
    }

    [Test]
    public void Create_User_NoExceptions()
    {
        try
        {
            var dpWrapper = new Mock<ISqlDapperWrapper>();
            dpWrapper.Setup(x => x.ExecuteScalar<int>(It.IsAny<string>(), It.IsAny<object?>())).Returns(1);

            var sqlprovider = new Mock<ISqlDataProvider>();
            sqlprovider.Setup(p => p.GetConnection()).Returns(dpWrapper.Object);


            var userService = new UserDataDpService(sqlprovider.Object);


            var user = userService.Create(new User() { Username="Moq", Password="123", Role=1});
            Assert.AreEqual(1, user.Id);
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
            var user = new User() {
                Id = 1,
                Username = "root",
                Password = "321"
            };

            var dpWrapper = new Mock<ISqlDapperWrapper>();
            dpWrapper.Setup(x => x.Execute(It.IsAny<string>(), It.IsAny<object?>())).Returns(1);

            var sqlprovider = new Mock<ISqlDataProvider>();
            sqlprovider.Setup(p => p.GetConnection()).Returns(dpWrapper.Object);

            var userService = new UserDataDpService(sqlprovider.Object);
            var response = userService.Update(user);
            
            Assert.IsTrue(response);
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }

}

