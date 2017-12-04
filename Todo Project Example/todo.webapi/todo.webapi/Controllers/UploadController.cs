using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

namespace todo.webapi.Controllers
{
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UploadController : Controller
    {
        private IHostingEnvironment _hosting;
        public UploadController(IHostingEnvironment hosting)
        {
            _hosting = hosting;
        }
        [HttpPost]
        [Route("api/Upload/PostFiles")]
        public async Task<IActionResult> PostFiles()
        {
            try
            {
                var files = Request.Form.Files;
                long size = files.Sum(f => f.Length);
                var repoPath = $"{_hosting.WebRootPath}/DocRepo";
                var fileName = Path.GetTempFileName();
                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        var filePath = System.IO.Path.Combine(repoPath, formFile.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/Upload/GetFile")]
        public async Task<IActionResult> GetFile(string fileName)
        {
            var repoPath = $"{_hosting.WebRootPath}/DocRepo";
            var filePath = System.IO.Path.Combine(repoPath, fileName);

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filePath), Path.GetFileName(filePath));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

    }
}

