using FilesServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace StaticFilesApi.Controllers
{
    public class FilesController : ControllerBase
    {
        private readonly IFilesProvider _filesProvider;

        public FilesController(IFilesProvider filesProvider)
        {
            _filesProvider = filesProvider;
        }


        [HttpGet]
        public ActionResult<IEnumerable<FileModel>> Get()
        {
            return null;
        }


        [HttpGet("[controller]/[action]/{fileId}")]
        public ActionResult<IEnumerable<FileModel>> Get(string fileId)
        {
            return null;
        }


        [HttpPost]
        public ActionResult<FileModel> Post(FileStream file, FileModel fileInfo)
        {
            return null;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<FileModel> Put(FileModel fileInfo)
        {
            throw new NotImplementedException();
        }


        [HttpGet]
        public ActionResult<FileModel> Delete(string id)
        {
            return null;
        }
    }
}
