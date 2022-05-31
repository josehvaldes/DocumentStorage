namespace DocuStorage.DataContent.S3;

using DocuStorage.Common.Data.Model;
using DocuStorage.Common.Data.Services;
using DocuStorage.Common;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;

public class S3DocumentContentService : IDocumentContentService
{
    private readonly IConfiguration _configuration;
    private AmazonS3Client _S3Client;
    private IS3Cache _s3Cache;
    
    private readonly string _s3BucketName;
    private readonly string _awsAccessKey;
    private readonly string _awsSecretKey;
    private readonly string _awsRegion;

    public S3DocumentContentService(IS3Cache s3Cache, IConfiguration configuration) 
    {
        _s3Cache = s3Cache;
        _configuration = configuration;

        _s3BucketName = _configuration.AWSBucketName();
        _awsAccessKey = _configuration.AWSAccessKey();
        _awsSecretKey = _configuration.AWSSecretKey();
        _awsRegion = _configuration.AWSRegion();

        AmazonS3Config config = new AmazonS3Config();
        config.RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(_awsRegion);

        _S3Client = new AmazonS3Client(_awsAccessKey, _awsSecretKey, config);
    }


    public async Task SaveDocContent(Document document)
    {
        var id = document.Id.ToString();
        if (_s3Cache.Contains(id)) 
        {
            await DeleteContent(document.Id);
            _s3Cache.Remove(id);
        }

        PutObjectRequest request = new PutObjectRequest() 
        {
            BucketName = _s3BucketName,
            Key = id,
        };
        
        using var ms = new MemoryStream(document.Content);
        request.InputStream = ms;
        request.Metadata.Add("FileName", document.Name);

        await _S3Client.PutObjectAsync(request);
        _s3Cache.Add(id, document.Content);
    }

    public async Task DeleteContent(int documentId)
    {
        var id = documentId.ToString();
        if (_s3Cache.Contains(id))
        {
            _s3Cache.Remove(id);
        }

        var deleteRequest = new DeleteObjectRequest()
        {
            BucketName = _s3BucketName,
            Key = id,
        };

        await _S3Client.DeleteObjectAsync(deleteRequest);
    }

    public void GetDocContent(Document document)
    {
        var id = document.Id.ToString();

        if (_s3Cache.Contains(id))
        {
            document.Content = _s3Cache.GetData(id);
        }
        else 
        {
            if (ExistsObject(id)) 
            {
                var request = new GetObjectRequest()
                {
                    BucketName = _s3BucketName,
                    Key = document.Id.ToString(),
                };

                using MemoryStream ms = new MemoryStream();
                using GetObjectResponse response = _S3Client.GetObjectAsync(request).Result;
                using Stream stream = response.ResponseStream;
                stream.CopyTo(ms);
                document.Content = ms.ToArray();
                _s3Cache.Add(id, document.Content);
            }
        }
    }

    public bool ExistsObject(string documentId)
    {
        try
        {
            var response = _S3Client.GetObjectMetadataAsync(
                new GetObjectMetadataRequest()
                {
                    BucketName = _s3BucketName,
                    Key = documentId
                }).Result;

            return true;
        }
        catch (Exception ex) 
        {
            var s3x = ex.InnerException as AmazonS3Exception;
            if ( s3x!=null ) 
            {
                if (s3x.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return false;
            }

            //status wasn't 'not found', so throw the exception
            throw;
        }        
    }
}
