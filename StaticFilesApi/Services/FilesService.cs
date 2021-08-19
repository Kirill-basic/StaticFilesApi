using FilesServices;
using System;
using System.Collections.Generic;
using System.IO;

namespace StaticFilesApi.Services
{
    public class FilesService : IFilesService
    {
        public FilesService(IFolderHandler folderHandler, IFilesService filesService)
        {

        }

        public List<FileModel> GetFileList()
        {
            var list = new List<FileModel> { new FileModel { Id = Guid.NewGuid().ToString(), Name = "test", Extension = ".png" } };

            return list;
        }


        public FileInfo EditFileInfo(FileInfo fileInfo)
        {
            throw new NotImplementedException();
        }


        public FileModel AddFile(FileStream file, FileModel fileInfo)
        {



            return new FileModel
            { 
                Id = Guid.NewGuid().ToString(), 
                Name = Path.GetFileName(file.Name), 
                Extension = Path.GetExtension(file.Name) 
            };
        }


        public FileModel DeleteFile(string fileId)
        {
            throw new NotImplementedException();
        }
    }
}
