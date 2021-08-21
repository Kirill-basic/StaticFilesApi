using FilesServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StaticFilesApi.Controllers
{
    public class FilesController : ControllerBase
    {
        private readonly IFilesService _filesService;

        public FilesController(IFilesService filesService)
        {
            _filesService = filesService;
        }


        [HttpGet("[controller]")]
        public async Task<ActionResult<IEnumerable<FileModel>>> GetAsync()
        {
            var list =  await _filesService.GetAsync();

            if (!list.Any())
            {
                return NotFound();
            }

            return Ok(list);
        }


        [HttpGet("[controller]/[action]/{fileId}")]
        public Task<Stream> Get(string fileId)
        {
            return null;
        }


        [HttpPost]
        public ActionResult<FileModel> Post(IFormFile file)
        {
            if (file == null)
            {
                return BadRequest("File was null");
            }

            var fileModel = _filesService.Post(file);

            return fileModel;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<FileModel> Put(FileModel fileInfo)
        {
            var fileModel = _filesService.Put(fileInfo);

            return fileModel;
        }


        [HttpGet]
        public ActionResult<FileModel> Delete(string id)
        {
            var fileModel = _filesService.Delete(id);

            return fileModel;
        }
    }
}
