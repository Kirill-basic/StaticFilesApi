using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FilesServices
{
    public interface IFilesService
    {
        public Task<IEnumerable<FileModel>> GetAsync();
        
        public Stream Get(string fileId);
        
        public FileModel Post(IFormFile file);
        
        public FileModel Put(FileModel model);
        
        public FileModel Delete(string fileId);
    }
}