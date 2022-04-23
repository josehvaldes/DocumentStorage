namespace DocuStore.Tests;
using DocuStorate.Data.Model;
using DocuStorate.Data.Services;
using NUnit.Framework;


/// <summary>
/// Unit for DocumentDataService class
/// </summary>
[TestFixture]
public class DocumentDataTests
{
    
    private const string Default_Filepath = @"data\favicon.ico";
    private const string Default_TestOutput = @"c:\personal\temp\favicon.ico";
    private const int DefaultGroupId = 1;
    private const int DefaultUserId = 1;
   

    private IDocumentDataService _documentService;
    private IGroupDataService _groupDataService;
    private Document _dummy;
    [SetUp]
    public void Setup()
    {
        _documentService = new DocumentDataService();
        _groupDataService = new GroupDataService();

        _dummy = _documentService.Create(GetDocument());
    }

    [TearDown]
    public void TearDown()
    {
        _documentService.Delete(_dummy.Id);
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
    public void Create_Document_NotEmpty() 
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
        var dbDocument = _documentService.Get(_dummy.Id);
        Assert.IsNotNull(dbDocument);
        File.WriteAllBytes(Default_TestOutput, _dummy.Content);
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
        int[] documents = new int[] { _dummy.Id };
        _documentService.AssignToUser(DefaultUserId, documents);

        var list = _documentService.GetByUserId(DefaultUserId);
        Assert.IsNotEmpty(list);
    }

    [Test]
    public void Get_DocumentsByGroupId_NotEmpty()
    {
        int[] documents = new int[] { _dummy.Id };
        _documentService.AssignToGroup(DefaultGroupId, documents);

        var list = _documentService.GetByGroupId(DefaultGroupId);
        Assert.IsNotEmpty(list);
    }

    [Test]
    public void AssignToUser_NoExceptions() 
    {
        int[] documents = new int[] { _dummy.Id };
        _documentService.AssignToUser(DefaultUserId, documents);
        Assert.IsTrue(true);
    }

    [Test]
    public void AssignToGroup_NoExceptions()
    {
        int[] documents = new int[] { _dummy.Id };
        _documentService.AssignToGroup(DefaultGroupId, documents);
        Assert.IsTrue(true);
    }


    [Test]
    public void Get_InGroupByUser_NotEmpty()
    {
        int[] documents = new int[] { _dummy.Id };
        int[] groups = new int[] { DefaultGroupId };
        
        _documentService.AssignToGroup(DefaultGroupId, documents);
        _groupDataService.AssignToUser(DefaultUserId, groups);

        var list = _documentService.GetInGroupsByUser(DefaultUserId);
        Assert.IsNotEmpty(list);
    }

    [Test]
    public void All_Get_Available_NotEmpty()
    {
        int[] documents = new int[] { _dummy.Id };
        _documentService.AssignToGroup(DefaultGroupId, documents);

        var list = _documentService.GetAllAvailableUser(DefaultUserId);
        Assert.IsNotEmpty(list);
    }

    [Test]
    public void Delete_NoExceptions()
    {
        try
        {
            var dbDocument = _documentService.Create(GetDocument());
            _documentService.Delete(dbDocument.Id);
            dbDocument = _documentService.Get(dbDocument.Id);
            Assert.IsNull(dbDocument);
        }
        catch (Exception)
        {
            Assert.Fail();
        }
    }
}

