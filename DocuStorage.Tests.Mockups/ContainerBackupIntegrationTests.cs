using DocuStorage.Common.Data.Model;
using DocuStorage.Common.Data.Services;
using DocuStorage.DAzure.DStorage;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace DocuStorage.Tests.Mockups;

[TestFixture]
public class ContainerBackupIntegrationTests
{
    private const string Default_Name = @"Default.jpg";
    private const string Default_Filepath = @"data\Default.jpg";
    private IConfiguration _configuration;

    [SetUp]
    public void Setup()
    {
        _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    }

    [Test]
    public void Backup_NoExceptions() 
    {
        var document = new Document() 
        {
            Id = 1,
            Name = Default_Name,
            Content = File.ReadAllBytes(Default_Filepath)
        };
        
        var service = new Mock<IDocumentDataService>();
        service.Setup(x=> x.Get(It.IsAny<int>())).Returns(document);

        var mirror = new Mock<IMirror<DocumentEntity>>();
        mirror.Setup(x => x.AddAsync(It.IsAny<DocumentEntity>())).Returns(Task.FromResult(new DocumentEntity()));

        var backup = new ContainerBackup(service.Object, _configuration, mirror.Object);

        try 
        {
            bool response = backup.Backup(document.Id).Result;    
        }
        catch(Exception ex) 
        {
            Assert.Fail(ex.Message);
        }
    }

    [Test]
    public void BackDelete_NoExceptions() 
    {
        var document = new Document()
        {
            Id = 1,
            Name = Default_Name,
            Content = File.ReadAllBytes(Default_Filepath)
        };

        var service = new Mock<IDocumentDataService>();
        service.Setup(x => x.Get(It.IsAny<int>())).Returns(document);

        var mirror = new Mock<IMirror<DocumentEntity>>();
        mirror.Setup(x=> x.AddAsync(It.IsAny<DocumentEntity>())).Returns(Task.FromResult(new DocumentEntity()));

        var backup = new ContainerBackup(service.Object, _configuration, mirror.Object);

        try
        {
            bool response = backup.Delete(document.Id).Result;
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }


    [Test]
    public void AddAsyncMirror_NoExceptions()
    {
        var document = new Document()
        {
            Id = 1,
            Name = Default_Name,
            Description = "Default description",
            Category = "text"
        };

        try 
        {
            var mirror = new DocumentTableMirror(_configuration);
            var response = mirror.AddAsync(Converter.Convert(document));
            response.Wait();

        }
        catch (Exception e) 
        {
            Assert.Fail(e.Message);
        }        
    }

    [Test]
    public void DeleteAsyncMirror_NoExceptions()
    {
        var document = new Document()
        {
            Id = 1,
            Name = Default_Name,
            Description = "Default description",
            Category = "text"
        };

        try
        {
            var mirror = new DocumentTableMirror(_configuration);
            var response = mirror.DeleteAsync(Converter.Convert(document));
            response.Wait();
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }

    [Test]
    public void GetAsyncMirror_NoExceptions()
    {
        var document = new Document()
        {
            Id = 1,
            Name = Default_Name,
            Description = "Default description",
            Category = "text"
        };

        try
        {
            var mirror = new DocumentTableMirror(_configuration);
            var addResponse = mirror.AddAsync(Converter.Convert(document)).Result;
            var response = mirror.GetAsync(document.Category, document.Id.ToString()).Result;

            Assert.AreEqual(document.Id.ToString(), response.Id);
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }

}

