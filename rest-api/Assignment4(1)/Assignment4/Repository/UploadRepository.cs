using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Options;
using IMDBAPI.Repository.Interfaces;
using Amazon.S3.Transfer;
using Amazon.S3;
using Amazon;
using Amazon.S3.Model;

namespace IMDBAPI.Repository
{
    public class UploadRepository:IUploadRepository
    {
        private readonly AmazonS3Client _amazonS3;
        private readonly string _bucketName;
        public UploadRepository(IOptions<ConnectionString> options) {
            _amazonS3 = new AmazonS3Client(options.Value.AccessKey, options.Value.SecretKey, RegionEndpoint.USEast2);
            _bucketName = options.Value.Bucket;
        }
        public async Task<string> Upload(IFormFile file)
        {

            var poster = $"posters/original/{Guid.NewGuid()}.jpg";
            using var stream = file.OpenReadStream();
            TransferUtility transferUtility = new TransferUtility(_amazonS3);
            await transferUtility.UploadAsync(stream, _bucketName, poster);
            return poster;
        }

        public string GenerateDownloadUrl(string objectKey, int durationMinutes = 30)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _bucketName,
                Key = objectKey,
                Verb = HttpVerb.GET,
                Expires = DateTime.UtcNow.AddMinutes(durationMinutes)
            };

            string presignedUrl = _amazonS3.GetPreSignedURL(request);

            return presignedUrl;
        }
    }
}
