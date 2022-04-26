namespace DocuStorage.Tests.Mockups;

using DocuStorate.Common.Data.Model;
using DocuStorage.Data.Dapper.Services;
using NUnit.Framework;
using DocuStorate.Common.Data.Services;

public class DocumentContentIntegrationTest
{
    private const string Default_Filepath = @"data\favicon.ico";
    private const string Default_TestOutput = @"c:\personal\temp\favicon.ico";
    private int DefaultDocumentId = 1;
    private IDocumentContentService _service;

    [SetUp]
    public void Setup() 
    {
        _service = new DocumentContentDpService(new SqlDapperProvider());
    }

    [Test]
    public void SaveDeleteContent_NoExceptions() 
    {
        var bytes = File.ReadAllBytes(Default_Filepath);
        var document = new Document()
        {
            Id = 1,
            Content = bytes
        };

        try
        {
            _service.SaveDocContent(document);
            Assert.Greater(document.Id, 0);
            _service.DeleteContent(document.Id);
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
            Id = DefaultDocumentId,
        };

        try
        {
            _service.GetDocContent(document);
            Assert.IsNotNull(document.Content);

            File.WriteAllBytes(Default_TestOutput, document.Content);
            Assert.IsTrue(File.Exists(Default_TestOutput));
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }

    }

}

