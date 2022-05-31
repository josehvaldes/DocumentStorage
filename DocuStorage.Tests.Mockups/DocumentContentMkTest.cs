namespace DocuStorage.Tests.Mockups;

using DocuStorage.Common.Data.Model;
using DocuStorage.Data.Dapper.Services;
using NUnit.Framework;
using DocuStorage.Common.Data.Services;
using Moq;

public class DocumentContentMkTest
{
    [Test]
    public void SaveContent_NoExceptions()
    {
        var document = new Document()
        {
            Id = 1,
        };

        try
        {
            var sqlprovider = new Mock<ISqlDataProvider>();
            var dpWrapper = new Mock<ISqlDapperWrapper>();

            dpWrapper.Setup(x => x.ExecuteScalar<int>(It.IsAny<string>(), It.IsAny<object?>())).Returns(1);
            sqlprovider.Setup(p => p.GetDocumentContentConnection()).Returns(dpWrapper.Object);

            var service = new DocumentContentDpService(sqlprovider.Object);
            service.SaveDocContent(document);
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }

    [Test]
    public void DeleteContent_NoExceptions()
    {
        try
        {
            var sqlprovider = new Mock<ISqlDataProvider>();
            var dpWrapper = new Mock<ISqlDapperWrapper>();

            dpWrapper.Setup(x => x.Execute(It.IsAny<string>(), It.IsAny<object?>())).Returns(1);
            sqlprovider.Setup(p => p.GetDocumentContentConnection()).Returns(dpWrapper.Object);

            var service = new DocumentContentDpService(sqlprovider.Object);
            service.DeleteContent(1);
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }

    [Test]
    public void GetContent_IsValid()
    {
        var document = new Document()
        {
            Id = 1,
        };

        try
        {
            var list = new List<object>() { new DocumentMoq { content = new byte[1] } };
            
            var dpWrapper = new Mock<ISqlDapperWrapper>();
            dpWrapper.Setup(x => x.Query(It.IsAny<string>(), It.IsAny<object?>())).Returns(list);

            var sqlprovider = new Mock<ISqlDataProvider>();
            sqlprovider.Setup(p => p.GetDocumentContentConnection()).Returns(dpWrapper.Object);

            var service = new DocumentContentDpService(sqlprovider.Object);
            
            service.GetDocContent(document);
            Assert.IsNotNull(document.Content);
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }

    }
}

