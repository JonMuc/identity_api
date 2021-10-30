using System.IO;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IAwsApiService
    {
        void CreateDirectory(string path);
        Task<string> CreateFileAsync(string path, Stream stream);
    }
}
