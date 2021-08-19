using System.Collections.Generic;
using System.IO;

namespace FilesServices
{
    public interface IFilesService
    {
        public FileModel Delete(string fileId);

        public IEnumerable<FileModel> Get();
        
        public Stream Get(string fileId);
        
        public FileModel Post(Stream file, FileModel model);
        
        public FileModel Put(Stream file, FileModel model);
    }
}