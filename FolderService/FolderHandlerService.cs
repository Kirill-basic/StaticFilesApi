using System;
using System.IO;
using System.Linq;

namespace FolderService
{

    public class FolderHandlerService : IFolderHandlerService
    {
        private const string _filesFolder = "Files";
        private const string _imagesFolder = "Images";
        private const string _audioFolder = "Audios";
        private const string _videoFolder = "Videos";
        private const string _commonFolder = "Common";

        private readonly string[] _imageExtensions = { ".png", "jpg", "jpeg", "gif" };
        private readonly string[] _videoExtensions = { ".avi", ".mp4", ".mkv" };
        private readonly string[] _audioExtensions = { ".mp3", ".vaw", ".flac" };

        public FolderHandlerService()
        {
            CreateFilesDirectory();
            CreateFilesSubFolders();
        }

        public string GetCompleteFilePath(string fileName, string fileExtension)
        {
            try
            {
                if (fileName is null)
                {
                    return null;
                }

                var fullFileName = Path.ChangeExtension(fileName, fileExtension);
                var fileSubFolder = GetFileSubFolder(fileName, fileExtension);

                return Path.Combine(fileSubFolder, fullFileName);
            }
            catch (Exception e)
            {
                throw;
            }
        }


        public string GetFileSubFolder(string fileName, string fileExtension)
        {
            string filesSubFolderPath;
            var filesDirectory = CreateFilesDirectory();

            switch (GetFileTypeByExtension(fileExtension))
            {
                case FileType.Image:
                    filesSubFolderPath = Path.Combine(filesDirectory, _imagesFolder);
                    break;
                case FileType.Audio:
                    filesSubFolderPath = Path.Combine(filesDirectory, _audioFolder);
                    break;
                case FileType.Video:
                    filesSubFolderPath = Path.Combine(filesDirectory, _videoFolder);
                    break;
                default:
                    filesSubFolderPath = Path.Combine(filesDirectory, _commonFolder);
                    break;
            }

            return filesSubFolderPath;
        }


        private string GetFilesDirectory()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), _filesFolder);
        }


        //TODO:create all folders in ctor and then return only folder names
        private string CreateFilesDirectory()
        {
            var filePath = GetFilesDirectory();
            Directory.CreateDirectory(filePath);

            return filePath;
        }


        private FileType GetFileTypeByExtension(string fileName)
        {
            var fileExtension = Path.GetExtension(fileName);

            if (_imageExtensions.Contains(fileExtension))
            {
                return FileType.Image;
            }

            if (_videoExtensions.Contains(fileExtension))
            {
                return FileType.Video;
            }

            if (_audioExtensions.Contains(fileExtension))
            {
                return FileType.Audio;
            }

            return FileType.Common;
        }


        private void CreateFilesSubFolders()
        {
            var filesDirectory = CreateFilesDirectory();

            var imagesFolderPath = Path.Combine(filesDirectory, _imagesFolder);

            var audioFolderPath = Path.Combine(filesDirectory, _audioFolder);

            var videoFolderPath = Path.Combine(filesDirectory, _videoFolder);

            var commonFilesFolderPath = Path.Combine(filesDirectory, _commonFolder);


            Directory.CreateDirectory(imagesFolderPath);
            Directory.CreateDirectory(audioFolderPath);
            Directory.CreateDirectory(videoFolderPath);
            Directory.CreateDirectory(commonFilesFolderPath);
        }
    }
}
