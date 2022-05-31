namespace DocuStorage.Tests.Mockups;

using DocuStorage.Common.Data.Model;
using DocuStorage.Data.Dapper.Services;
using NUnit.Framework;
using DocuStorage.Common.Data.Services;
using Microsoft.Extensions.Configuration;

[TestFixture]
public class DocumentContentIntegrationTest
{
    private const string Default_Filepath = @"data\favicon.ico";
    private const string Default_TestOutput = @"c:\personal\temp\favicon.ico";
    private int DefaultDocumentId = 1;
    private IDocumentContentService _service;
    private IConfiguration _configuration;
    

    [SetUp]
    public void Setup() 
    {
        _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _service = new DocumentContentDpService(new SqlDapperProvider(_configuration));
    }

    [Test]
    public void SaveDeleteContent_NoExceptions() 
    {
        var bytes = File.ReadAllBytes(Default_Filepath);
        var document = new Document()
        {
            Id = 2,
            Content = bytes
        };

        try
        {
            _service.SaveDocContent(document);
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

