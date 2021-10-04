using Domain.Interfaces.Models;
using Domain.Models.Enums;
using System;

namespace Domain.Models
{
    public class BaseModel : IBaseModel
    {
        public long Id { get; set; }
        public DateTime? CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
        public long IdAtualizadoPor { get; set; }
        public long IdCriadoPor { get; set; }
        public StatusRegistro StatusRegistro { get; set; }
    }
}
