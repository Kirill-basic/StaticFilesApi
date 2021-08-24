using FolderService;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FilesServices
{
    public class FilesService : IFilesService
    {
        private readonly IFolderHandlerService _folderHandler;
        private readonly IFilesProvider _filesProvider;
        private readonly IFileModelProvider _modelProvider;

        public FilesService(IFolderHandlerService folderHandler, IFilesProvider filesProvider, IFileModelProvider modelProvider)
        {
            _folderHandler = folderHandler;
            _filesProvider = filesProvider;
            _modelProvider = modelProvider;
        }

        public async Task<IEnumerable<FileModel>> GetAsync()
        {
            return await _modelProvider.GetAsync();
        }


        public async Task<Stream> GetAsync(string fileId)
        {
            try
            {
                var fileInfo = await _modelProvider.GetAsync(fileId);

                if (fileInfo == null)
                {
                    return null;
                }

                var completeFilePath = _folderHandler.GetCompleteFilePath(fileInfo.Id, fileInfo.Extension);

                if (completeFilePath is null)
                {
                    throw new Exception("Couldn't combine file path");
                }

                var file = _filesProvider.GetFile(completeFilePath);

                return file;
            }
            catch (Exception e)
            {
                throw;
            }
        }


        //пляж ай-петри или пляж марат
        //TODO:Return file name instead of file id
        public async Task<FileModel> PostAsync(IFormFile file)
        {
            try
            {
                if (file is null)
                {
                    return null;
                }

                var fileName = Path.GetFileNameWithoutExtension(file.FileName);

                if (fileName is null)
                {
                    throw new Exception("File name was empty");
                }

                var fileExtension = Path.GetExtension(file.FileName);
                var fileId = Guid.NewGuid().ToString();
                var completeFilePath = _folderHandler.GetCompleteFilePath(fileId, fileExtension);

                if (completeFilePath is null)
                {
                    throw new ArgumentNullException(nameof(completeFilePath));
                }

                var fileModel = new FileModel()
                {
                    Id = fileId,
                    Name = fileName,
                    Extension = fileExtension,
                };

                var savedFileModel = await _modelProvider.PostAsync(fileModel);
                await _filesProvider.PostFileAsync(file, completeFilePath);

                return savedFileModel;
            }
            catch (Exception e)
            {
                throw;
            }
        }


        public async Task<FileModel> PutAsync(FileModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            try
            {
                var updatedFileModel = await _modelProvider.PutAsync(model);

                return updatedFileModel;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<FileModel> DeleteAsync(string fileId)
        {
            var fileModel = await _modelProvider.DeleteAsync(fileId);

            if (fileModel is null)
            {
                return null;
            }

            var completeFilePath = _folderHandler.GetCompleteFilePath(fileModel.Id, fileModel.Extension);

            await _filesProvider.DeleteFile(completeFilePath);

            return fileModel;
        }
    }
}
