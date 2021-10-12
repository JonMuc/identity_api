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
        public async Task<List<Noticia>> ListarNoticiaFavoritoAsync(long idUsuario)
        {
            var sql = @" SELECT tn.Id, tn.AtualizadoEm , tn.CriadoEm , tn.IdAtualizadoPor , tn.Titulo , 
                                tn.Fonte ,tn.HoraAtras ,tn.TipoNoticia ,tn.IdCriadoPor , tn.UrlImage , 
                                tn.Link ,tn.OrigemNoticia , tn.StatusRegistro
                            FROM TBL_NOTICIA tn 
                            JOIN CRZ_NOTICIA_USUARIO cnu 
	                            ON tn.Id = cnu.IdNoticia 
                            JOIN TBL_USUARIO tu 
	                            ON tu.Id = cnu.IdUsuario 
                            WHERE cnu.IdUsuario = @IdUsuario";

            var obj = new NoticiaFavorito
            {
                IdUsuario = idUsuario
            };

            return (List<Noticia>)await _unitOfWork.Connection.QueryAsync<Noticia>(sql, obj, _unitOfWork?.Transaction);
        }
    }
}
