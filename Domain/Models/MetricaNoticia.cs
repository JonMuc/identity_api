using Domain.Models.Enums;
using Domain.Models.Request;

namespace Domain.Models
{
    public class MetricaNoticia : BaseModel
    {
        public long IdNoticia { get; set; }
        public long IdUsuario { get; set; }  
       
    }
}
