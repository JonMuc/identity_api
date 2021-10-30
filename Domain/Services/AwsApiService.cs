using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System.IO;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class AwsApiService : IAwsApiService
    {
        //CREDENCIAIS DA AWS
        private AmazonS3Client _s3client;
        private string _awsBucket = "noticia-app";
        private string _awsAccessKey = "AKIAYBCS7JXIZMW5BDEC";
        private string _awsSecretKey = "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";

        public AwsApiService()
        { }


        private AmazonS3Client S3Client
        {
            get
            {
                if (_s3client == null)
                {
                    _s3client = new AmazonS3Client(_awsAccessKey, _awsSecretKey, RegionEndpoint.USEast2);
                }
                return _s3client;
            }
        }

        public void CreateDirectory(string path)
        {
            var info = new DirectoryInfo(path);
            if (!info.Exists)
            {
                info.Create();
            }
        }


        public async Task<string> CreateFileAsync(string path, Stream stream)
        {
            var putRequest = new PutObjectRequest()
            {
                BucketName = _awsBucket,
                Key = path,
                InputStream = stream
            };
            var response = await S3Client.PutObjectAsync(putRequest);
            return _awsBucket;
        }
    }

}
