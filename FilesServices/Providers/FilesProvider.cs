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
            if (completeFilePath is null)
            {
                return null;
            }

            try
            {
                var stream = File.OpenRead(completeFilePath);

                return stream;
            }
            catch (Exception e)
            {
                throw;
            }
        }


        public async Task PostFileAsync(IFormFile file, string completeFilePath)
        {
            if (completeFilePath is null)
            {
                throw new ArgumentNullException(nameof(completeFilePath));
            }

            try
            {
                using var stream = new FileStream(completeFilePath, FileMode.Create, FileAccess.Write);
                await file.CopyToAsync(stream);
            }
            catch (Exception e)
            {
                throw;
            }
        }


        public Task DeleteFile(string completeFilePath)
        {
            if (completeFilePath is null)
            {
                throw new ArgumentNullException(nameof(completeFilePath));
            }

            try
            {
                File.Delete(completeFilePath);

                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
