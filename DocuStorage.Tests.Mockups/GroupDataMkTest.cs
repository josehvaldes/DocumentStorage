namespace DocuStorage.Tests.Mockups;

using DocuStorage.Data.Dapper.Services;
using DocuStorage.Common.Data.Model;
using DocuStorage.Common.Data.Services;
using NUnit.Framework;
using Moq;

[TestFixture]
public class GroupDataMkTest
{

    [Test]
    public void Create_Group_NotEmpty()
    {
        try {
            
            var dpWrapper = new Mock<ISqlDapperWrapper>();
            dpWrapper.Setup(x => x.ExecuteScalar<int>(It.IsAny<string>(), It.IsAny<object?>())).Returns(1);

            var sqlprovider = new Mock<ISqlDataProvider>();
            sqlprovider.Setup(p => p.GetConnection()).Returns(dpWrapper.Object);

            var groupDataService = new GroupDataDpService(sqlprovider.Object);
            var response = groupDataService.Create(new Group(){Name = "Dapper Group"});
            Assert.AreEqual(response.Id, 1);
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
            var dpWrapper = new Mock<ISqlDapperWrapper>();
            dpWrapper.Setup(x => x.Execute(It.IsAny<string>(), It.IsAny<object?>())).Returns(1);

            var sqlprovider = new Mock<ISqlDataProvider>();
            sqlprovider.Setup(p => p.GetConnection()).Returns(dpWrapper.Object);

            var groupDataService = new GroupDataDpService(sqlprovider.Object);

            groupDataService.Delete(1);
        }
        catch(Exception e)
        {
            Assert.Fail(e.Message);
        }
    }

    [Test]
    public void Get_Groups_IsNotEmpty()
    {
        var dpWrapper = new Mock<ISqlDapperWrapper>();
        
        dpWrapper.Setup(x => x.Query<Group>(It.IsAny<string>(), It.IsAny<object>())).
            Returns(new List<Group>() { new Group() { Name="Moq Test", Id=1 } });

        var sqlprovider = new Mock<ISqlDataProvider>();
        sqlprovider.Setup(p => p.GetConnection()).Returns(dpWrapper.Object);

        var groupDataService = new GroupDataDpService(sqlprovider.Object);

        var list = groupDataService.GetAll();
        Assert.IsNotEmpty(list);
    }

    [Test]
    public void GetGroupByUser_IsNotEmpty()
    {
        var dpWrapper = new Mock<ISqlDapperWrapper>();
        dpWrapper.Setup(x => x.Query<Group>(It.IsAny<string>(), It.IsAny<object>())).
            Returns(new List<Group>() { new Group() { Name = "Moq Test", Id = 1 } });


        var sqlprovider = new Mock<ISqlDataProvider>();
        sqlprovider.Setup(p => p.GetConnection()).Returns(dpWrapper.Object);

        var groupDataService = new GroupDataDpService(sqlprovider.Object);

        var list = groupDataService.GetByUser(1);
        Assert.IsNotEmpty(list);
    }

    [Test]
    public void AssignToUser_NoExceptions()
    {
        try
        {
            var dpWrapper = new Mock<ISqlDapperWrapper>();
            dpWrapper.Setup(x => x.Execute(It.IsAny<string>(), It.IsAny<object>())).Returns(1);

            var sqlprovider = new Mock<ISqlDataProvider>();
            sqlprovider.Setup(p => p.GetConnection()).Returns(dpWrapper.Object);

            var groupDataService = new GroupDataDpService(sqlprovider.Object);

            int[] groups = new int[] { 1 };
            groupDataService.AssignToUser(1, groups);

        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }

}

