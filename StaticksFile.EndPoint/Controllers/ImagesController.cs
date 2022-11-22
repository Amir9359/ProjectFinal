using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StaticksFile.EndPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IHostingEnvironment _environment;

        public ImagesController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost]
        public IActionResult Post(string apiKey)
        {
            //قرار دادن فیلدی برای secretKey  و کنترل کردن ان قبل از انجام ان که کاربران احراز شده دسترسی داشته باشند
            if (apiKey != "secretKey")
            {
                return BadRequest();
            }

            try
            {
                var Files = Request.Form.Files;
                var folderPath = Path.Combine("Resources", "images");
                var path = Path.Combine(Directory.GetCurrentDirectory(), folderPath);
                if (Files != null)
                {
                    // upload
                    return Ok(UpaloadFiles(Files));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
                throw new Exception("upload Images Failed", ex);
            }
        }

        private UploadDto UpaloadFiles(IFormFileCollection files)
        {
            var newName = Guid.NewGuid().ToString();
            var date = DateTime.Now;
            string folder = $@"Resources\images\{date.Year}\{date.Year}-{date.Month}\";
            var uploadsRootFolder = Path.Combine(_environment.WebRootPath, folder);
            if (!Directory.Exists(uploadsRootFolder))
            {
                Directory.CreateDirectory(uploadsRootFolder);
            }

            List<string> address = new List<string>();
            foreach (var file in files)
            {
                if (file != null && file.Length > 0)
                {
                    string fileName = newName + file.FileName;
                    var filePath = Path.Combine(uploadsRootFolder, fileName);

                    using (var fileStream = new FileStream(filePath , FileMode.Create ))
                    {
                        file.CopyTo(fileStream);
                    }
                    address.Add(folder + fileName);
                }
            }
            return new UploadDto()
            {
                FileNameAddress = address,
                Status = true,
            };
        }

        public class UploadDto
        {
            public bool Status { get; set; }
            public List<string> FileNameAddress { get; set; }
        }
    }
}
