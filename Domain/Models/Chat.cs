using System;

namespace Domain.Models
{
    public class Chat : BaseModel
    {
        public long IdUsuarioEnvio { get; set; }
        public long IdUsuarioRecebe { get; set; }   
        public string Mensagem { get; set; } 
        public DateTime? Leitura { get; set; } 
    }
}
