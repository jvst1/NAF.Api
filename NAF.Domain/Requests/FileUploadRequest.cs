using Microsoft.AspNetCore.Http;

namespace NAF.Domain.Requests
{
    public class FileUploadRequest
    {
        public Guid CodigoUsuario { get; set; }
        public IFormFile File { get; set; }

        public FileUploadRequest(IFormFile file)
        {
            File = file;
        }
    }
}
