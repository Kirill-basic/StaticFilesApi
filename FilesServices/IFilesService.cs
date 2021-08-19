using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FilesServices
{
    public interface IFilesService
    {
        public Task<IEnumerable<FileModel>> GetAsync();
        
        public Stream Get(string fileId);
        
        public FileModel Post(Stream file, FileModel model);
        
        public FileModel Put(Stream file, FileModel model);
        
        public FileModel Delete(string fileId);
    }
}