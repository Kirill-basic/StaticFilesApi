using Microsoft.AspNetCore.Mvc;
using System;


namespace StaticFilesApi.Controllers
{
    public class FilesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetFiles()
        {
            throw new NotImplementedException();
        }


        [HttpGet("{action}/{fileId}")]
        public IActionResult GetFile(string fileId)
        {
            throw new NotImplementedException();
        }


        [HttpPost]
        public IActionResult SaveFile()
        {
            throw new NotImplementedException();
        }


        [HttpPut]
        public IActionResult Edit()
        {
            throw new NotImplementedException();
        }

        
        [HttpDelete]
        public IActionResult Delete()
        {
            throw new NotImplementedException();
        }
    }
}
