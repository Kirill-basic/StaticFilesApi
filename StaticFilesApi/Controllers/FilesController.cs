using FilesServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        //Here is the link to postman collection for testing API
        //https://www.getpostman.com/collections/8e7fede6f4550f684db0
        //also possible to test from Swagger UI

        private readonly IFilesService _filesService;
        private readonly ILogger<FilesController> _logger;

        public FilesController(IFilesService filesService, ILogger<FilesController> logger)
        {
            _filesService = filesService;
            _logger = logger;
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
            _logger.LogInformation("Invoking get method");
            
            try
            {
                var list = await _filesService.GetAsync();

                if (!list.Any())
                {
                    _logger.LogInformation("list was empty, returning 404");
                    return NotFound();
                }

                _logger.LogInformation("Files found successfully, returning 200");
                return Ok(list);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting files, returning 400");
                return BadRequest();
            }
        }


        //TODO:don't like returning null think of what can i do with this
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
            _logger.LogInformation("Invoking Get by id method");

            if (fileId is null)
            {
                _logger.LogWarning("fileId is null, returning null");
                return null;
            }

            _logger.LogInformation($"Getting file with id {fileId}");

            try
            {
                var stream = await _filesService.GetAsync(fileId);

                if (stream is null)
                {
                    _logger.LogInformation("stream was null, returning null");
                    return null;
                }

                _logger.LogInformation("stream received successfully");
                return stream;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting file");
                return null;
            }
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
            _logger.LogInformation("Invoking post method");

            if (file is null)
            {
                _logger.LogInformation("File was null, returning 400");
                return BadRequest("File is null");
            }

            try
            {
                var fileModel = await _filesService.PostAsync(file);

                if (fileModel is null)
                {
                    _logger.LogError("fileModel is null, retuning 400");
                    return BadRequest("Error saving file, please try again later");
                }

                _logger.LogInformation("file posted successfully, returning 200");
                return Ok(fileModel);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "error posing file, returning 400");
                return BadRequest();
            }
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
            _logger.LogInformation("Invoking Put method");

            if (jsonModel is null)
            {
                _logger.LogInformation("jsonmodel was null, returning 400");
                return BadRequest("Model was empty");
            }

            try
            {
                var model = JsonConvert.DeserializeObject<FileModel>(jsonModel);

                if (model is null)
                {
                    _logger.LogInformation("model was null, returning 400");
                    //TODO:check incorrect model
                    return BadRequest("Incorrect model");
                }

                var updatedFileModel = await _filesService.PutAsync(model);

                if (updatedFileModel is null)
                {
                    _logger.LogInformation("File wasn't found, returning 404");
                    return NotFound();
                }

                _logger.LogInformation("File was upadted successfully, returning 200");
                return Ok(updatedFileModel);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error updating file, returning 400");
                return BadRequest();
            }
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
            _logger.LogInformation("Invoking Delete method");

            if (fileId is null)
            {
                _logger.LogInformation("FileId was null, returning 400");
                return BadRequest("FileId was empty");
            }

            try
            {
                var fileModel = await _filesService.DeleteAsync(fileId);

                if (fileModel is null)
                {
                    _logger.LogInformation("FileModel was null, returning 404");
                    return NotFound();
                }

                _logger.LogInformation("File was deleted successfully, returning 200");
                return Ok(fileModel);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error deleting file, returning 400");
                return BadRequest();
            }
        }
    }
}
