using FilesServices;
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
