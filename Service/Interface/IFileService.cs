using Microsoft.AspNetCore.Http;

namespace Service.Interface
{
    public interface IFileService
    {
        Task<string> SaveFile(IFormFile file,string fileName);
    }
}
