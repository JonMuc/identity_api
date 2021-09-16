using Domain.Models.Enums;
using System;

namespace Domain.Interfaces.Models
{
    public interface IBaseModel
    {
        long Id { get; set; }
        DateTime? CriadoEm { get; set; }
        DateTime? AtualizadoEm { get; set; }
        long IdAtualizadoPor { get; set; }
        long IdCriadoPor { get; set; }
        StatusRegistro Status { get; set; }
    }
}
