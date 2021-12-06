using Domain.Models;
using Domain.Models.Dto;
using Domain.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IMetricaRepository
    {        
        Task ContabilizarClickAsync(long idUsuario, long idNoticia);
    }
}
