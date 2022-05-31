using DocuStorage.Common.Data.Model;
using DocuStorage.Common.Data.Services;
using DocuStorage.DAzure.DStorage;
using Moq;
using NUnit.Framework;

namespace DocuStorage.Tests.Mockups;

[TestFixture]
public class ContainerBackupIntegrationTests
{
    private const string Default_Name = @"Default.jpg";
    private const string Default_Filepath = @"data\Default.jpg";

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

        var backup = new ContainerBackup(service.Object);

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

        var backup = new ContainerBackup(service.Object);

        try
        {
            bool response = backup.Delete(document.Id).Result;
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

}

