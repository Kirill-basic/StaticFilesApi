using Microsoft.AspNetCore.Mvc;
using StaticFilesApi.Services;
using System;
using System.Collections.Generic;
using System.IO;
using FileInfo = StaticFilesApi.Models.FileInfo;

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
        public ActionResult<IEnumerable<FileInfo>> Get()
        {
            return _filesService.GetFileList();
        }


        [HttpGet("[controller]/[action]/{fileId}")]
        public ActionResult<IEnumerable<FileInfo>> Get(string fileId)
        {
            throw new NotImplementedException();
        }


        [HttpPost]
        public ActionResult<FileInfo> Create(FileStream file, FileInfo fileInfo)
        {
            return _filesService.AddFile(file, fileInfo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<FileInfo> Edit(FileInfo fileInfo)
        {
            return _filesService.EditFileInfo(fileInfo);
        }

        
        [HttpGet]
        public ActionResult<FileInfo> Delete(string id)
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
