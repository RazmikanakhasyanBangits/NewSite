using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Service.Interface;

namespace Service.Impl
{
    public class FileService : IFileService
    {
        private readonly IHostEnvironment env;

        public FileService(IHostEnvironment env)
        {
            this.env = env;
        }

        public async Task<string> SaveFile(IFormFile file,string fileName)
        {
            var dir = env.ContentRootPath;
            var filePath = Path.Combine("./wwwroot/Files",fileName+".jpg");
            using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            await  file.CopyToAsync(fileStream);

            return fileName + ".jpg";
        }
    }
}
