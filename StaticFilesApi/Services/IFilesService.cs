using FilesServices;
using System.Collections.Generic;
using System.IO;

namespace StaticFilesApi.Services
{
    public interface IFilesService
    {
        public List<FileModel> GetFileList();

        public FileModel AddFile(FileStream file, FileModel fileInfo);

        public FileModel EditFileInfo(FileModel fileInfo);

        public FileModel DeleteFile(string fileId);
    }
}
