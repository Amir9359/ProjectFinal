using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;

namespace Infrastructure.ExternalApi.ImageServer
{
    public interface IImageUploadService
    {
        List<string> UploadImages(List<IFormFile> files);
    }
    public class ImageUploadService : IImageUploadService
    {

        public List<string> UploadImages(List<IFormFile> files)
        {
            var client = new RestClient("https://static.am-khoshbakht.ir/api/Images?apiKey=secretKey");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            foreach (var file in files)
            {
                byte[] bytes;
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    bytes = ms.ToArray();
                }

                request.AddFile(file.FileName, bytes, file.FileName, file.ContentType);
            }

            IRestResponse response = client.Execute(request);
            var upload = JsonConvert.DeserializeObject<UploadDto>(response.Content);
            return upload.FileNameAddress;
        }
    }
    public class UploadDto
    {
        public bool Status { get; set; }
        public List<string> FileNameAddress { get; set; }
    }
}