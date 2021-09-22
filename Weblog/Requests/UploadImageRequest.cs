using Microsoft.AspNetCore.Http;

namespace Weblog.Requests
{
    public class UploadImageRequest
    { 
        public string Description { get; set; }
        public IFormFile Image { get; set; }

        public UploadImageRequest()
        {
        }
        public UploadImageRequest(string description, IFormFile image)
        {
            Description = description;
            Image = image;
        }
    }
}
