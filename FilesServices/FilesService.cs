﻿using FolderService;
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
            var fileInfo = await _modelProvider.GetAsync(fileId);
            if (fileInfo == null)
            {
                return null;
            }

            var completeFilePath = _folderHandler.GetCompleteFilePath(fileInfo.Id, fileInfo.Extension);

            var file = _filesProvider.GetFile(completeFilePath);

            return file;
        }


        //пляж ай-петри или пляж марат
        public async Task<FileModel> PostAsync(IFormFile file)
        {
            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            var fileExtension = Path.GetExtension(file.FileName);
            var fileId = Guid.NewGuid().ToString();
            var completeFilePath = _folderHandler.GetCompleteFilePath(fileName, fileExtension);

            var fileModel = new FileModel()
            {
                Id = fileId,
                Name = fileName,
                Extension = fileExtension,
            };

            await _modelProvider.PostAsync(fileModel);
            await _filesProvider.PostFileAsync(file, completeFilePath);

            return fileModel;
        }


        public async Task<FileModel> PutAsync(FileModel model)
        {
            var updatedFileModel = await _modelProvider.PutAsync(model);

            return updatedFileModel;
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
