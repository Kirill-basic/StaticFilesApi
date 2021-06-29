using System;
using System.Collections.Generic;
using System.IO;
using FileInfo = StaticFilesApi.Models.FileInfo;

namespace StaticFilesApi.Services
{
    public class FilesService : IFilesService
    {
        public List<FileInfo> GetFileList()
        {
            var list = new List<FileInfo> { new FileInfo { Id = Guid.NewGuid().ToString(), Name = "test", Extension = ".png" } };

            return list;
        }


        public FileInfo EditFileInfo(FileInfo fileInfo)
        {
            throw new NotImplementedException();
        }


        public FileInfo AddFile(FileStream file, FileInfo fileInfo)
        {
            return new FileInfo 
            { 
                Id = Guid.NewGuid().ToString(), 
                Name = Path.GetFileName(file.Name), 
                Extension = Path.GetExtension(file.Name) 
            };
        }


        public FileInfo DeleteFile(string fileId)
        {
            throw new NotImplementedException();
        }
    }
}
