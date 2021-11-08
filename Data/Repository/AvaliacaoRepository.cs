using Dapper;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class AvaliacaoRepository : BaseRepository, IAvaliacaoRepository
    {
        public AvaliacaoRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<long> AdicionarAsync(Comentario request)
        {
            var sql = @" INSERT INTO tbl_comentario (IdComentario, IdNoticia, Mensagem, CriadoEm, AtualizadoEm, IdAtualizadoPor, IdCriadoPor, StatusRegistro)
                                    VALUES (@IdComentario, @IdNoticia, @Mensagem, @CriadoEm, @AtualizadoEm, @IdAtualizadoPor, @IdCriadoPor, @StatusRegistro)
                         SELECT @@IDENTITY";

            return await _unitOfWork.Connection.ExecuteScalarAsync<long>(sql, request, _unitOfWork?.Transaction);
        }

        public async Task<IEnumerable<Comentario>> ListarComentariosNoticiaAsync(long idNoticia)
        {
            var sql = @" SELECT * FROM TBL_COMENTARIO WHERE IdNoticia = @idNoticia";
            return await _unitOfWork.Connection.QueryAsync<Comentario>(sql, new { idNoticia }, _unitOfWork?.Transaction);
        }
    }
}
