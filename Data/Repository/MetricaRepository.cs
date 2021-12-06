using Dapper;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using System.Threading.Tasks;
using System;

namespace Data.Repository
{
    public class MetricaRepository : BaseRepository, IMetricaRepository
    {
        public MetricaRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
               
        public async Task ContabilizarClickAsync(long idUsuario, long idNoticia)
        {
            var sql = @" INSERT INTO crz_metrica_noticia (IdUsuario, IdNoticia, CriadoEm, AtualizadoEm, IdAtualizadoPor, IdCriadoPor, StatusRegistro)
                                    VALUES (@IdUsuario, @IdNoticia, @CriadoEm, @AtualizadoEm, @IdAtualizadoPor, @IdCriadoPor, @StatusRegistro)";
            
            var request = new MetricaNoticia()
            {
                IdNoticia = idNoticia,
                IdUsuario = idUsuario,
                CriadoEm = DateTime.Now
            };

            await _unitOfWork.Connection.ExecuteScalarAsync(sql, request, _unitOfWork?.Transaction);
        }
    }
}
