using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Weblog.Services
{
    public class FileHandlerService
    {
        private readonly IHostingEnvironment _env;
        public FileHandlerService(IHostingEnvironment env)
        {
            _env = env;
        }
        public async Task<string> Store(IFormFile file)
        {
            if (file.Length > 0)
            {
                var address = Path.Combine("images", file.FileName);
                var filePath = Path.Combine(_env.WebRootPath, address);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }

                return address;
            }

            return "";

        }


    }
}
