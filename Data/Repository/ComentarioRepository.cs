using Dapper;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Models.Dto;
using Domain.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ComentarioRepository : BaseRepository, IComentarioRepository
    {
        public ComentarioRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task ExcluirAsync(Comentario request)
        {
            var sql = @" UPDATE TBL_COMENTARIO SET StatusRegistro = 1, AtualizadoEm = @AtualizadoEm  WHERE Id = @Id";
            await _unitOfWork.Connection.ExecuteScalarAsync<long>(sql, request, _unitOfWork?.Transaction);
        }

        public async Task<long> AdicionarAsync(Comentario request)
        {
            var sql = @" INSERT INTO tbl_comentario (IdComentario, IdNoticia, Mensagem, CriadoEm, AtualizadoEm, IdAtualizadoPor, IdCriadoPor, StatusRegistro)
                                    VALUES (@IdComentario, @IdNoticia, @Mensagem, @CriadoEm, @AtualizadoEm, @IdAtualizadoPor, @IdCriadoPor, @StatusRegistro)
                         SELECT @@IDENTITY";

            return await _unitOfWork.Connection.ExecuteScalarAsync<long>(sql, request, _unitOfWork?.Transaction);
        }

        public async Task<IEnumerable<ViewComentario>> ListarComentariosNoticiaAsync(ComentarioRequest request)
        {
            var sql = @"select come.Id as IdComentario, usua.Id as IdUsuario, come.Mensagem, usua.Nome, usua.Foto as UrlFoto, 
                        come.CriadoEm as DataComentario,
                        (select count(*) from TBL_AVALIACAO where IdComentario = come.Id and TipoAvaliacao = 1 and StatusRegistro = 0) as QuantidadeLike,
                        (select count(*) from TBL_AVALIACAO where IdComentario = come.Id and TipoAvaliacao = 2 and StatusRegistro = 0) as QuantidadeDeslike,
                        (select TipoAvaliacao from TBL_AVALIACAO WHERE  IdComentario = come.Id and IdUsuario = @IdUsuario and StatusRegistro = 0) as ComentarioAvaliado,
                        (select count(*) from TBL_COMENTARIO where IdComentario = come.Id and StatusRegistro = 0) AS QuantidadeSubComentario
                        from TBL_COMENTARIO come
                        inner join TBL_USUARIO usua
                        on come.IdCriadoPor = usua.Id WHERE come.IdNoticia = @IdNoticia
						and come.IdComentario = 0
                        and come.StatusRegistro = 0 ORDER BY DataComentario asc OFFSET @PageIndex ROWS FETCH NEXT @PageSize ROWS ONLY";
            return await _unitOfWork.Connection.QueryAsync<ViewComentario>(sql, request, _unitOfWork?.Transaction);
        }

        public async Task<IEnumerable<ViewComentario>> ListarComentariosNoticiaDeslogadoAsync(ComentarioRequest request)
        {
            var sql = @"select come.Id as IdComentario, usua.Id as IdUsuario, come.Mensagem, usua.Nome, usua.Foto as UrlFoto, 
                        come.CriadoEm as DataComentario,
                        (select count(*) from TBL_AVALIACAO where IdComentario = come.Id and TipoAvaliacao = 1 and StatusRegistro = 0) as QuantidadeLike,
                        (select count(*) from TBL_AVALIACAO where IdComentario = come.Id and TipoAvaliacao = 2 and StatusRegistro = 0) as QuantidadeDeslike,
                        (select count(*) from TBL_COMENTARIO where IdComentario = come.Id and StatusRegistro = 0) AS QuantidadeSubComentario
                        from TBL_COMENTARIO come
                        inner join TBL_USUARIO usua
                        on come.IdCriadoPor = usua.Id WHERE come.IdNoticia = @IdNoticia 
                        and come.IdComentario = 0
                        and come.StatusRegistro = 0 ORDER BY DataComentario asc OFFSET @PageIndex ROWS FETCH NEXT @PageSize ROWS ONLY";
            return await _unitOfWork.Connection.QueryAsync<ViewComentario>(sql, request, _unitOfWork?.Transaction);
        }

        public async Task<IEnumerable<Comentario>> ListarComentariosComentarioAsync(long idComentario)
        {
            var sql = @" SELECT * FROM TBL_COMENTARIO WHERE IdComentario = @idComentario and StatusRegistro = 0";
            return await _unitOfWork.Connection.QueryAsync<Comentario>(sql, new { idComentario }, _unitOfWork?.Transaction);
        }

        public async Task<IEnumerable<ViewComentario>> ListarComentariosComentarioAsync(ComentarioRequest request)  //testar
        {
            var sql = @" SELECT  come.Id, come.IdComentario ,come.Mensagem, come.CriadoEm AS DataComentario, usua.Id as IdUsuario, usua.Nome, usua.Foto as UrlFoto,
						                            (SELECT count(*) FROM TBL_AVALIACAO WHERE IdComentario = come.Id AND TipoAvaliacao = 1 AND StatusRegistro = 0) AS QuantidadeLike,
                                                    (SELECT count(*) FROM TBL_AVALIACAO WHERE IdComentario = come.Id AND TipoAvaliacao = 2 AND StatusRegistro = 0) AS QuantidadeDeslike,
                                                    (SELECT TipoAvaliacao FROM TBL_AVALIACAO WHERE IdComentario = come.Id AND IdUsuario = @IdUsuario AND StatusRegistro = 0) AS ComentarioAvaliado                        
                            FROM TBL_COMENTARIO come
                            INNER JOIN TBL_USUARIO usua
                            ON come.IdCriadoPor = usua.Id
                            WHERE come.IdComentario = @IdComentario and
                            come.StatusRegistro = 0
                            ORDER BY DataComentario ASC OFFSET @PageIndex ROWS FETCH NEXT @PageSize ROWS ONLY";

            var response = await _unitOfWork.Connection.QueryAsync<ViewComentario>(sql, request, _unitOfWork?.Transaction);
            return response;
        }

        public async Task<Comentario> GetComentarioById(long idComentario)
        {
            var sql = @" SELECT * FROM tbl_comentario WHERE Id = @Id";
            var obj = new Comentario
            {
                Id = idComentario
            };

            return await _unitOfWork.Connection.QueryFirstOrDefaultAsync<Comentario>(sql, obj, _unitOfWork?.Transaction);
        }
    }
}
