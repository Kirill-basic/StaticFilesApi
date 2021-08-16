using System;
using System.IO;

namespace FolderService
{
    public class FolderHandlerService : IFolderHandler
    {
        private const string _filesFolder = "Files";

        public string CreateDirectory()
        {
            var filePath = GetDirectory();
            Directory.CreateDirectory(filePath);

            return filePath;
        }
            

        private string GetDirectory()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(currentDirectory, _filesFolder);

            return filePath;
        }


        public string GetCompleteFilePath(string fileName, string fileExtension)
        {
            var fileDirectory = GetDirectory();
            var completeFilePath = Path.Combine(fileDirectory, fileName, fileExtension);

            return completeFilePath;
        }
    }
}
