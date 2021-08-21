using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FilesServices
{
    public class FilesProvider : IFilesProvider
    {
        public Stream GetFile(string completeFilePath)
        {
            using (var stream = new FileStream(completeFilePath, FileMode.Open))
            {
                return stream;
            }
        }


        public async Task PostFileAsync(IFormFile file, string completeFilePath)
        {
            using var stream = new FileStream(completeFilePath, FileMode.Create, FileAccess.Write);
            await file.CopyToAsync(stream);
        }


        public Task DeleteFile(string completeFilePath)
        {
            File.Delete(completeFilePath);

            return Task.CompletedTask;
        }
    }
}
