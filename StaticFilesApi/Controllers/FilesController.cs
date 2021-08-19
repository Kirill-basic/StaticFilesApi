using FilesServices;
using Microsoft.AspNetCore.Mvc;
using StaticFilesApi.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace StaticFilesApi.Controllers
{
    public class FilesController : ControllerBase
    {
        private readonly IFilesService _filesService;

        public FilesController(IFilesService filesService)
        {
            _filesService = filesService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<FileModel>> Get()
        {
            return _filesService.GetFileList();
        }


        [HttpGet("[controller]/[action]/{fileId}")]
        public ActionResult<IEnumerable<FileModel>> Get(string fileId)
        {
            throw new NotImplementedException();
        }


        [HttpPost]
        public ActionResult<FileModel> Post(FileStream file, FileModel fileInfo)
        {
            return _filesService.AddFile(file, fileInfo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<FileModel> Put(FileModel fileInfo)
        {
            return _filesService.EditFileInfo(fileInfo);
        }

        
        [HttpGet]
        public ActionResult<FileModel> Delete(string id)
        {
            var deleteResult = _filesService.DeleteFile(id);

            if (deleteResult is null)
            {
                return NotFound();
            }

            return deleteResult;
        }
    }
}
