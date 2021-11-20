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

        public async Task<long> AdicionarAvaliacaoAsync(Avaliacao request)
        {
            var sql = @" INSERT INTO tbl_avaliacao (IdComentario, IdNoticia, IdUsuario, TipoAvaliacao, CriadoEm, AtualizadoEm, IdAtualizadoPor, IdCriadoPor, StatusRegistro)
                                    VALUES (@IdComentario, @IdNoticia, @IdUsuario, @TipoAvaliacao, @CriadoEm, @AtualizadoEm, @IdAtualizadoPor, @IdCriadoPor, @StatusRegistro)
                         SELECT @@IDENTITY";

            return await _unitOfWork.Connection.ExecuteScalarAsync<long>(sql, request, _unitOfWork?.Transaction);
        }

        public async Task ExcluirAvaliacaoComentarioAsync(long idUsuario, long idComentario)
        {
            var sql = @" UPDATE tbl_avaliacao
                            SET StatusRegistro = 1
                            WHERE IdUsuario = @IdUsuario AND IdComentario = @IdComentario";
            var obj = new Avaliacao
            {
                IdUsuario = idUsuario,
                IdComentario = idComentario
            };

            await _unitOfWork.Connection.ExecuteAsync(sql, obj, _unitOfWork?.Transaction);
        }

        public async Task ExcluirAvaliacaoNoticiaAsync(long idUsuario, long idNoticia)
        {
            var sql = @" UPDATE tbl_avaliacao
                            SET StatusRegistro = 1
                            WHERE IdUsuario = @IdUsuario AND IdNoticia = @IdNoticia";
            var obj = new Avaliacao
            {
                IdUsuario = idUsuario,
                IdNoticia = idNoticia
            };

            await _unitOfWork.Connection.ExecuteAsync(sql, obj, _unitOfWork?.Transaction);
        }

        public async Task<List<Avaliacao>> GetAvaliacaoByUsuarioNoticia(long idUsuario, long idNoticia)
        {
            var sql = @" SELECT * FROM tbl_avaliacao WHERE IdUsuario = @IdUsuario AND IdNoticia = @IdNoticia";
            var obj = new Avaliacao
            {
                IdUsuario = idUsuario,
                IdNoticia = idNoticia
            };

            return (List<Avaliacao>)await _unitOfWork.Connection.QueryAsync<Avaliacao>(sql, obj, _unitOfWork?.Transaction);
        }

        public async Task<List<Avaliacao>> GetAvaliacaoByUsuarioComentario(long idUsuario, long idComentario)
        {
            var sql = @" SELECT * FROM tbl_avaliacao WHERE IdUsuario = @IdUsuario AND IdComentario = @IdComentario";
            var obj = new Avaliacao
            {
                IdUsuario = idUsuario,
                IdComentario = idComentario
            };

            return (List<Avaliacao>)await _unitOfWork.Connection.QueryAsync<Avaliacao>(sql, obj, _unitOfWork?.Transaction);
        }
    }
}
