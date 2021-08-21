using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace FilesServices
{
    public interface IFilesProvider
    {
        Stream GetFile(string completeFilePath);
        
        Task PostFileAsync(IFormFile file, string completeFilePath);

        Task DeleteFile(string completeFilePath);
    }
}