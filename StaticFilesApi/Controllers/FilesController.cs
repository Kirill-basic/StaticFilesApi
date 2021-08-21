using FilesServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            var list = await _filesService.GetAsync();

            if (!list.Any())
            {
                return NotFound();
            }

            return Ok(list);
        }


        [HttpGet("[controller]/{fileId}")]
        public async Task<Stream> Get(string fileId)
        {
            var stream = await _filesService.GetAsync(fileId);

            return stream;
        }


        [HttpPost("[controller]")]
        public async Task<ActionResult<FileModel>> PostAsync([FromForm] IFormFile file)
        {
            if (file == null)
            {
                return BadRequest("File was null");
            }

            var fileModel = await _filesService.PostAsync(file);

            return fileModel;
        }


        //TODO:remove antiforgery later
        [HttpPut("[controller]")]
        public async Task<ActionResult<FileModel>> PutAsync([FromForm] string jsonModel)
        {
            var model = JsonConvert.DeserializeObject<FileModel>(jsonModel);

            var updatedFileModel = await _filesService.PutAsync(model);

            return updatedFileModel;
        }


        [HttpDelete("[controller]/{id}")]
        public async Task<ActionResult<FileModel>> DeleteAsync([FromRoute] string id)
        {
            var fileModel = await _filesService.DeleteAsync(id);

            return fileModel;
        }
    }
}
