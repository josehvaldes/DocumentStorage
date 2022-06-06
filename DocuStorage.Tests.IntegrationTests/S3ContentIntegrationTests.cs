using DocuStorage.Common.Data.Model;
using DocuStorage.Common.Data.Services;
using DocuStorage.DataContent.S3;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace DocuStorage.Tests.Mockups;

[TestFixture]
public class S3ContentIntegrationTests
{
    private const string Default_Name = @"Default.jpg";
    private const string Default_Filepath = @"data\Default.jpg";
    private const string Default_TestOutput = @"c:\personal\temp\Default.jpg";
    private IS3Cache _cache;
    private IConfiguration _configuration;

    [SetUp]
    public void Setup() 
    {
        var builder = new ConfigurationBuilder().AddUserSecrets<S3ContentIntegrationTests>();
        _configuration = builder.Build();
        _cache = new RedisCache();
    }

    [Test]
    public void SaveDocument_NoExceptions() 
    {

        var bytes = File.ReadAllBytes(Default_Filepath);
        var document = new Document()
        {
            Id = 1,
            Content = bytes,
            Name = Default_Name
        };

        try 
        {
            var s3Service = new S3DocumentContentService(_cache, _configuration);
            var task = s3Service.SaveDocContent(document);
            task.Wait();
        }
        catch(Exception ex) 
        {
            Assert.Fail(ex.Message);
        }
    }

    [Test]
    public void DeleteDocument_NoExceptions() 
    {
        try
        {
            string documentId = "1";
            _cache.Add(documentId, new byte[1]);

            var s3Service = new S3DocumentContentService(_cache, _configuration);
            var task = s3Service.DeleteContent(int.Parse(documentId) );
            task.Wait();
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [Test]
    public void GetDocContent_ValidContent() 
    {
        try 
        {
            var document = new Document()
            {
                Id = 1,
                Name = Default_Name,
                Content = File.ReadAllBytes(Default_Filepath),
            };

            var s3Service = new S3DocumentContentService(_cache, _configuration);

            s3Service.SaveDocContent(document).Wait();

            document.Content = null; // empty the content
            
            s3Service.GetDocContent(document);

            Assert.IsNotNull(document.Content);

            File.WriteAllBytes(Default_TestOutput, document.Content);
            
            Assert.IsTrue(File.Exists(Default_TestOutput));
        } 
        catch (Exception ex) 
        {
            Assert.Fail(ex.Message);   
        }
    }


    [Test]
    public void GetDocContent_InValidContent()
    {
        try
        {
            var document = new Document()
            {
                Id = 2,
            };

            var s3Service = new S3DocumentContentService(_cache, _configuration);
            s3Service.GetDocContent(document);
            Assert.IsNull(document.Content);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

}
