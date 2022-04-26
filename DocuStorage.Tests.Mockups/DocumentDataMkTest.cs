namespace DocuStorage.Tests.Mockups;

using DocuStorage.Data.Dapper.Services;
using DocuStorate.Common.Data.Model;
using DocuStorate.Common.Data.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;



[TestFixture]
public class DocumentDataMkTest
{
    [Test]
    public void Create_Document_NotEmpty()
    {
        var dpWrapper = new Mock<ISqlDapperWrapper>();
        dpWrapper.Setup(x => x.ExecuteScalar<int>(It.IsAny<string>(), It.IsAny<object?>())).Returns(1);

        var docContentMoq = new Mock<IDocumentContentService>();
        docContentMoq.Setup(p=> p.SaveDocContent(It.IsAny<Document>()));

        var sqlprovider = new Mock<ISqlDataProvider>();
        sqlprovider.Setup(p => p.GetConnection()).Returns(dpWrapper.Object);

        var documentService = new DocumentDataDpService(sqlprovider.Object, docContentMoq.Object);
        var response = documentService.Create(new Document() { Name="A Name", Description="Def Description" });

        Assert.IsNotNull(response);
        Assert.AreEqual(response.Id, 1);
    }

    public void Delete_Document_NotEmpty()
    {
        try
        {
            var dpWrapper = new Mock<ISqlDapperWrapper>();
            dpWrapper.Setup(x => x.Execute(It.IsAny<string>(), It.IsAny<object?>())).Returns(1);

            var docContentMoq = new Mock<IDocumentContentService>();
            docContentMoq.Setup(p => p.SaveDocContent(It.IsAny<Document>()));

            var sqlprovider = new Mock<ISqlDataProvider>();
            sqlprovider.Setup(p => p.GetConnection()).Returns(dpWrapper.Object);

            var documentService = new DocumentDataDpService(sqlprovider.Object, docContentMoq.Object);
            documentService.Delete(1);
        }
        catch (Exception ex) 
        {
            Assert.Fail(ex.Message);
        }
        

        
    }

    [Test]
    public void Get_Document_NotNull_ValidContent()
    {

        var dpWrapper = new Mock<ISqlDapperWrapper>();
        dpWrapper.Setup(x => x.Query<Document>(It.IsAny<string>(), It.IsAny<object?>())).Returns(new List<Document>() { new Document() { Id=1, Content = new byte[1] } });

        var docContentMoq = new Mock<IDocumentContentService>();
        docContentMoq.Setup(p => p.GetDocContent(It.IsAny<Document>()));

        var sqlprovider = new Mock<ISqlDataProvider>();
        sqlprovider.Setup(p => p.GetConnection()).Returns(dpWrapper.Object);

        var documentService = new DocumentDataDpService(sqlprovider.Object, docContentMoq.Object);
        var document = documentService.Get(1);

        Assert.IsNotNull(document);
        Assert.IsNotNull(document?.Content);
    }

    [Test]
    public void Get_Documents_IsNotEmpty()
    {
        var dpWrapper = new Mock<ISqlDapperWrapper>();
        dpWrapper.Setup(x => x.Query<Document>(It.IsAny<string>(), It.IsAny<object?>())).Returns(new List<Document>() { new Document() { Id = 1, Content = new byte[1] } });

        var docContentMoq = new Mock<IDocumentContentService>();
        docContentMoq.Setup(p => p.GetDocContent(It.IsAny<Document>()));

        var sqlprovider = new Mock<ISqlDataProvider>();
        sqlprovider.Setup(p => p.GetConnection()).Returns(dpWrapper.Object);

        var documentService = new DocumentDataDpService(sqlprovider.Object, docContentMoq.Object);

        var list = documentService.GetAll();
        Assert.IsNotEmpty(list);
    }

    [Test]
    public void Get_DocumentsByUserId_NotEmpty()
    {
        var dpWrapper = new Mock<ISqlDapperWrapper>();
        dpWrapper.Setup(x => x.Query<Document>(It.IsAny<string>(), It.IsAny<object?>())).Returns(new List<Document>() { new Document() { Id = 1, Content = new byte[1] } });

        var docContentMoq = new Mock<IDocumentContentService>();
        docContentMoq.Setup(p => p.GetDocContent(It.IsAny<Document>()));

        var sqlprovider = new Mock<ISqlDataProvider>();
        sqlprovider.Setup(p => p.GetConnection()).Returns(dpWrapper.Object);
        var documentService = new DocumentDataDpService(sqlprovider.Object, docContentMoq.Object);

        var list = documentService.GetByUserId(1);
        Assert.IsNotEmpty(list);
    }

    [Test]
    public void Get_DocumentsByGroupId_NotEmpty()
    {
        var dpWrapper = new Mock<ISqlDapperWrapper>();
        dpWrapper.Setup(x => x.Query<Document>(It.IsAny<string>(), It.IsAny<object?>())).Returns(new List<Document>() { new Document() { Id = 1, Content = new byte[1] } });

        var docContentMoq = new Mock<IDocumentContentService>();
        docContentMoq.Setup(p => p.GetDocContent(It.IsAny<Document>()));

        var sqlprovider = new Mock<ISqlDataProvider>();
        sqlprovider.Setup(p => p.GetConnection()).Returns(dpWrapper.Object);
        var documentService = new DocumentDataDpService(sqlprovider.Object, docContentMoq.Object);


        var list = documentService.GetByGroupId(1);
        Assert.IsNotEmpty(list);
    }

    [Test]
    public void AssignToUser_NoExceptions()
    {
        var dpWrapper = new Mock<ISqlDapperWrapper>();
        dpWrapper.Setup(x => x.Execute(It.IsAny<string>(), It.IsAny<object?>())).Returns(1);

        var docContentMoq = new Mock<IDocumentContentService>();
        docContentMoq.Setup(p => p.GetDocContent(It.IsAny<Document>()));

        var sqlprovider = new Mock<ISqlDataProvider>();
        sqlprovider.Setup(p => p.GetConnection()).Returns(dpWrapper.Object);
        var documentService = new DocumentDataDpService(sqlprovider.Object, docContentMoq.Object);


        documentService.AssignToUser(1, new int[1]);
        Assert.IsTrue(true);
    }

    [Test]
    public void AssignToGroup_NoExceptions()
    {
        var dpWrapper = new Mock<ISqlDapperWrapper>();
        dpWrapper.Setup(x => x.Execute(It.IsAny<string>(), It.IsAny<object?>())).Returns(1);

        var docContentMoq = new Mock<IDocumentContentService>();
        docContentMoq.Setup(p => p.GetDocContent(It.IsAny<Document>()));

        var sqlprovider = new Mock<ISqlDataProvider>();
        sqlprovider.Setup(p => p.GetConnection()).Returns(dpWrapper.Object);
        var documentService = new DocumentDataDpService(sqlprovider.Object, docContentMoq.Object);

        documentService.AssignToGroup(1, new int[1]);
        Assert.IsTrue(true);
    }


    [Test]
    public void Get_InGroupByUser_NotEmpty()
    {

        var dpWrapper = new Mock<ISqlDapperWrapper>();
        dpWrapper.Setup(x => x.Query<DocumentGroup>(It.IsAny<string>(), It.IsAny<object?>())).Returns(new List<DocumentGroup>() { new DocumentGroup() });

        var docContentMoq = new Mock<IDocumentContentService>();
        docContentMoq.Setup(p => p.GetDocContent(It.IsAny<Document>()));

        var sqlprovider = new Mock<ISqlDataProvider>();
        sqlprovider.Setup(p => p.GetConnection()).Returns(dpWrapper.Object);
        var documentService = new DocumentDataDpService(sqlprovider.Object, docContentMoq.Object);

        var list = documentService.GetInGroupsByUser(1);
        Assert.IsNotEmpty(list);
    }

    [Test]
    public void All_Get_Available_NotEmpty()
    {
        var dpWrapper = new Mock<ISqlDapperWrapper>();
        dpWrapper.Setup(x => x.Query<DocumentGroup>(It.IsAny<string>(), It.IsAny<object?>())).Returns(new List<DocumentGroup>() { new DocumentGroup() });

        var docContentMoq = new Mock<IDocumentContentService>();
        docContentMoq.Setup(p => p.GetDocContent(It.IsAny<Document>()));

        var sqlprovider = new Mock<ISqlDataProvider>();
        sqlprovider.Setup(p => p.GetConnection()).Returns(dpWrapper.Object);
        var documentService = new DocumentDataDpService(sqlprovider.Object, docContentMoq.Object);


        var list = documentService.GetAllAvailableUser(1);
        Assert.IsNotEmpty(list);
    }
}
