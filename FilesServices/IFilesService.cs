using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FilesServices
{
    public interface IFilesService
    {
        public Task<IEnumerable<FileModel>> GetAsync();
        
        public Task<Stream> GetAsync(string fileId);
        
        public Task<FileModel> PostAsync(IFormFile file);
        
        public Task<FileModel> PutAsync(FileModel model);
        
        public Task<FileModel> DeleteAsync(string fileId);
    }
}