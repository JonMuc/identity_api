using System.IO;

namespace Domain.Models.Request
{
    public class UploadImagemRequest
    {
        public Stream FileStreamIO { get; set; }
        public long IdUsuario { get; set; }
    }
}
