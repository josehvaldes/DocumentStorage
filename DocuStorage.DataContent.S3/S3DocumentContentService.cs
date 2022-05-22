namespace DocuStorage.DataContent.S3;

using DocuStorage.Common.Data.Model;
using DocuStorage.Common.Data.Services;
using DocuStorage.Common;
using Amazon.S3;
using Amazon.S3.Model;

public class S3DocumentContentService : IDocumentContentService
{
    private AmazonS3Client _S3Client;

    private readonly string _s3BucketName = Configuration.AWSBucketName();
    private readonly string _awsAccessKey = Configuration.AWSAccessKey();
    private readonly string _awsSecretKey = Configuration.AWSSecretKey();
    private readonly string _awsRegion = Configuration.AWSRegion();

    public S3DocumentContentService() 
    {
        AmazonS3Config config = new AmazonS3Config();
        config.RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(_awsRegion);

        _S3Client = new AmazonS3Client(_awsAccessKey, _awsSecretKey, config);
    }

    public async Task SaveDocContent(Document document)
    {
        PutObjectRequest request = new PutObjectRequest() 
        {
            BucketName = _s3BucketName,
            Key = document.Id.ToString(),
        };
        
        using var ms = new MemoryStream(document.Content);
        request.InputStream = ms;
        request.Metadata.Add("FileName", document.Name);

        await _S3Client.PutObjectAsync(request);
    }

    public async Task DeleteContent(int documentId)
    {
        var deleteRequest = new DeleteObjectRequest() 
        {
            BucketName = _s3BucketName,
            Key = documentId.ToString(),
        };

        await _S3Client.DeleteObjectAsync(deleteRequest);
    }

    public void GetDocContent(Document document)
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
    }
}
