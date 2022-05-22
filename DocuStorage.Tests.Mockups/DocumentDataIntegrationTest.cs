namespace DocuStorage.Tests.Mockups;

using DocuStorage.Data.Dapper.Services;
using DocuStorage.Common.Data.Model;
using DocuStorage.Common.Data.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

[TestFixture]
public class DocumentDataIntegrationTest
{

    private const string Default_Filepath = @"data\favicon.ico";
    private const string Default_TestOutput = @"c:\personal\temp\favicon.ico";
    private const int DefaultGroupId = 1;
    private const int DefaultUserId = 1;
    private const int DefaultDocumentId = 1;


    private IDocumentDataService _documentService;
    private IGroupDataService _groupDataService;

    [SetUp]
    public void Setup()
    {
        var sqlDataProvider = new SqlDapperProvider();
        _documentService = new DocumentDataDpService(sqlDataProvider, new DocumentContentDpService(sqlDataProvider));
        _groupDataService = new GroupDataDpService(sqlDataProvider);
    }

    private Document GetDocument()
    {
        var bytes = File.ReadAllBytes(Default_Filepath);
        Document document = new Document()
        {
            Name = "favicon.ico",
            Category = "icons",
            Description = "my icon",
            Content = bytes
        };

        return document;
    }

    [Test]
    public void CreateDelete_Document_NotEmpty()
    {
        var document = GetDocument();
        var response = _documentService.Create(document);
        Assert.IsNotNull(response);
        Assert.Greater(response.Id, 0);
        _documentService.Delete(document.Id);
    }

    [Test]
    public void Get_Document_NotNull_ValidContent()
    {
        var dbDocument = _documentService.Get(DefaultDocumentId);
        Assert.IsNotNull(dbDocument);
        File.WriteAllBytes(Default_TestOutput, dbDocument.Content);
        Assert.IsTrue(File.Exists(Default_TestOutput));
    }

    [Test]
    public void Get_Documents_IsNotEmpty()
    {
        var list = _documentService.GetAll();
        Assert.IsNotEmpty(list);
    }

    [Test]
    public void Get_DocumentsByUserId_NotEmpty()
    {
        int[] documents = new int[] { DefaultDocumentId };
        _documentService.AssignToUser(DefaultUserId, documents);

        var list = _documentService.GetByUserId(DefaultUserId);
        Assert.IsNotEmpty(list);
    }

    [Test]
    public void Get_DocumentsByGroupId_NotEmpty()
    {
        int[] documents = new int[] { DefaultDocumentId };
        _documentService.AssignToGroup(DefaultGroupId, documents);

        var list = _documentService.GetByGroupId(DefaultGroupId);
        Assert.IsNotEmpty(list);
    }

    [Test]
    public void AssignToUser_NoExceptions()
    {
        int[] documents = new int[] { DefaultDocumentId };
        _documentService.AssignToUser(DefaultUserId, documents);
        Assert.IsTrue(true);
    }

    [Test]
    public void AssignToGroup_NoExceptions()
    {
        int[] documents = new int[] { DefaultDocumentId };
        _documentService.AssignToGroup(DefaultGroupId, documents);
        Assert.IsTrue(true);
    }


    [Test]
    public void Get_InGroupByUser_NotEmpty()
    {
        int[] documents = new int[] { DefaultDocumentId };
        int[] groups = new int[] { DefaultGroupId };

        _documentService.AssignToGroup(DefaultGroupId, documents);
        _groupDataService.AssignToUser(DefaultUserId, groups);

        var list = _documentService.GetInGroupsByUser(DefaultUserId);
        Assert.IsNotEmpty(list);
    }

    [Test]
    public void All_Get_Available_NotEmpty()
    {
        int[] documents = new int[] { DefaultDocumentId };
        _documentService.AssignToGroup(DefaultGroupId, documents);

        var list = _documentService.GetAllAvailableUser(DefaultUserId);
        Assert.IsNotEmpty(list);
    }
}
