using StaticFilesApi.Models;
using System.Collections.Generic;
using System.IO;
using FileInfo = StaticFilesApi.Models.FileInfo;

namespace StaticFilesApi.Services
{
    public interface IFilesService
    {
        public List<FileInfo> GetFileList();

        public FileInfo AddFile(FileStream file, FileInfo fileInfo);

        public FileInfo EditFileInfo(FileInfo fileInfo);

        public FileInfo DeleteFile(string fileId);
    }
}
