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


        /// <summary>
        /// Get a list of FileModels
        /// </summary>
        /// <param></param>
        /// <returns>
        /// StatusCode with a list of FileModels
        /// </returns>
        /// <response code="200">If successful</response>
        /// <response code="400">If error occurs</response>
        /// <response code="404">If no files found</response>
        [HttpGet("[controller]")]
        public async Task<ActionResult<IEnumerable<FileModel>>> GetAsync()
        {
            try
            {
                var list = await _filesService.GetAsync();

                if (!list.Any())
                {
                    return NotFound();
                }

                return Ok(list);
            }
            catch (Exception e)
            {

                return BadRequest();
            }
        }


        /// <summary>
        /// Download file by id
        /// </summary>
        /// <param name="fileId">Id of a file</param>
        /// <returns>
        /// Stream of a file
        /// </returns>
        /// <response code="200">If successful</response>
        /// <response code="400">If error occurs</response>
        /// <response code="404">If file wasn't found</response>
        [HttpGet("[controller]/{fileId}")]
        public async Task<Stream> Get(string fileId)
        {
            var stream = await _filesService.GetAsync(fileId);

            return stream;
        }


        /// <summary>
        /// Upload file to the api and save it
        /// </summary>
        /// <param name="file">Uploaded file</param>
        /// <returns>
        /// StatusCode with a FileModel of the saved file
        /// </returns>
        /// <response code="200">If successful</response>
        /// <response code="400">If error occurs</response>
        /// <response code="404">If no files found</response>
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


        /// <summary>
        /// Update file name or file extension
        /// </summary>
        /// <param name="jsonModel">FileModel as JSON</param>
        /// <returns>
        /// statusCode with an updated FileModel
        /// </returns>
        /// <response code="200">If successful</response>
        /// <response code="400">If error occurs</response>
        /// <response code="404">If no files found</response>
        [HttpPut("[controller]")]
        public async Task<ActionResult<FileModel>> PutAsync([FromForm] string jsonModel)
        {
            var model = JsonConvert.DeserializeObject<FileModel>(jsonModel);

            var updatedFileModel = await _filesService.PutAsync(model);

            return updatedFileModel;
        }


        /// <summary>
        /// Delete a file by its id
        /// </summary>
        /// <param name="fileId">Id of a file to delete</param>
        /// <returns>
        /// statusCode with a FileModel of a deleted file
        /// </returns>
        /// <response code="200">If successful</response>
        /// <response code="400">If error occurs</response>
        /// <response code="404">If no files found</response>
        [HttpDelete("[controller]/{fileId}")]
        public async Task<ActionResult<FileModel>> DeleteAsync([FromRoute] string fileId)
        {
            var fileModel = await _filesService.DeleteAsync(fileId);

            return fileModel;
        }
    }
}
