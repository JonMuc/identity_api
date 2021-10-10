using Dapper;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Models.Enums;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class NoticiaFavoritoRepository : BaseRepository, INoticiaFavoritoRepository
    {
        public NoticiaFavoritoRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<long> AdicionarNoticiaFavoritoAsync(NoticiaFavorito request)
        {
            var sql = @" INSERT INTO crz_noticia_usuario (AtualizadoEm, CriadoEm, IdAtualizadoPor, IdCriadoPor, StatusRegistro, IdNoticia, IdUsuario)
                                    VALUES (@AtualizadoEm, @CriadoEm, @IdAtualizadoPor, @IdCriadoPor, @StatusRegistro, @IdNoticia, @IdUsuario)
                         SELECT @@IDENTITY";

            return await _unitOfWork.Connection.ExecuteScalarAsync<long>(sql, request, _unitOfWork?.Transaction);
        }

        public async Task<NoticiaFavorito> AtualizarNoticiaFavoritoAsync(NoticiaFavorito noticiaFavorito)
        {
            var sql = @" UPDATE crz_noticia_usuario
                            SET AtualizadoEm = @AtualizadoEm, IdAtualizadoPor = @IdAtualizadoPor, StatusRegistro = @StatusRegistro,
                                IdNoticia = @IdNoticia, IdUsuario = @IdUsuario
                            WHERE Id = @Id";

            await _unitOfWork.Connection.ExecuteAsync(sql, noticiaFavorito, _unitOfWork?.Transaction);
            return noticiaFavorito;
        }

        public async Task DeletarNoticiaFavoritoAsync(long idNoticiaFavorito)
        {
            var sql = @" UPDATE crz_noticia_usuario
                            SET StatusRegistro = 1
                            WHERE Id = @Id";
            var obj = new
            {
                Id = idNoticiaFavorito
            };

            await _unitOfWork.Connection.ExecuteAsync(sql, obj, _unitOfWork?.Transaction);

        }

        public async Task<NoticiaFavorito> GetNoticiaFavoritoById(long idNoticiaFavorito)
        {
            var sql = @" SELECT * 
                            FROM crz_noticia_usuario
                            WHERE Id = @Id";
            var obj = new NoticiaFavorito
            {
                Id = idNoticiaFavorito
            };

            return await _unitOfWork.Connection.QueryFirstOrDefaultAsync<NoticiaFavorito>(sql, obj, _unitOfWork?.Transaction);
        }
        //public async Task<List<Noticia>> ListarNoticiaPorTipoAsync(TipoNoticia tipoNoticia)
        //{

        //SELECT *
        //    FROM crz_noticia_usuario As crz
        //    INNER JOIN tbl_noticia AS noti
        //    ON crz.IdNoticia = noti.Id
        //    INNER JOIN tbl_usuario AS usu
        //    ON crz.IdUsuario = usu.Id
        //    WHERE crz.Id = @Id;

        //    var sql = @" SELECT * FROM tbl_noticia WHERE TipoNoticia = @TipoNoticia";
        //    var obj = new Noticia
        //    {
        //        TipoNoticia = tipoNoticia
        //    };

        //   return (List<Noticia>)await _unitOfWork.Connection.QueryAsync<Noticia>(sql, obj, _unitOfWork?.Transaction);
        //}
    }
}
