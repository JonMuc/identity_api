using Dapper;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ComentarioRepository : BaseRepository, IComentarioRepository
    {
        public ComentarioRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<long> AdicionarAsync(Comentario request)
        {
            var sql = @" INSERT INTO tbl_comentario (IdComentario, IdNoticia, Mensagem, CriadoEm, AtualizadoEm, IdAtualizadoPor, IdCriadoPor, StatusRegistro)
                                    VALUES (@IdComentario, @IdNoticia, @Mensagem, @CriadoEm, @AtualizadoEm, @IdAtualizadoPor, @IdCriadoPor, @StatusRegistro)
                         SELECT @@IDENTITY";

            return await _unitOfWork.Connection.ExecuteScalarAsync<long>(sql, request, _unitOfWork?.Transaction);
        }

        public async Task<IEnumerable<ViewComentario>> ListarComentariosNoticiaAsync(long idNoticia)
        {
            var sql = @"select come.Id as IdComentario, usua.Id as IdUsuario, come.Mensagem, usua.Nome, usua.Foto, come.CriadoEm as DataComentario
                        from TBL_COMENTARIO come
                        inner join TBL_USUARIO usua
                        on come.IdCriadoPor = usua.Id WHERE come.IdNoticia = @idNoticia";
            return await _unitOfWork.Connection.QueryAsync<ViewComentario>(sql, new { idNoticia }, _unitOfWork?.Transaction);
        }

        public async Task<IEnumerable<Comentario>> ListarComentariosComentarioAsync(long idComentario)
        {
            var sql = @" SELECT * FROM TBL_COMENTARIO WHERE IdComentario = @idComentario";
            return await _unitOfWork.Connection.QueryAsync<Comentario>(sql, new { idComentario }, _unitOfWork?.Transaction);
        }
    }
}
