﻿using System.IO;
using System.Threading.Tasks;

namespace FilesServices
{
    interface IFilesProvider
    {
        Task DeleteFile(string fileId);
        Task EditFile(Stream file, FileModel model);
        Task PostFile(Stream file, FileModel model);
    }
}