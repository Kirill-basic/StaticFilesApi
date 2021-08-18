using System;
using System.IO;
using System.Threading.Tasks;

namespace FilesServices
{
    class FilesProvider : IFilesProvider
    {
        public Task PostFile(Stream file, FileModel model)
        {
            throw new NotImplementedException();
        }


        public Task EditFile(Stream file, FileModel model)
        {
            throw new NotImplementedException();
        }


        public Task DeleteFile(string fileId)
        {
            throw new NotImplementedException();
        }
    }
}
