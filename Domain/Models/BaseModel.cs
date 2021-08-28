using Domain.Interfaces.Models;
using System;

namespace Domain.Models
{
    public class BaseModel : IBaseModel
    {
        public long Id { get; set; }
        public DateTime? CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }
}
