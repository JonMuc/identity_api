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
    public class NoticiaRepository : BaseRepository, INoticiaRepository
    {
        public NoticiaRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<long> AdicionarNoticiaAsync(Noticia request)
        {
            var sql = @" INSERT INTO tbl_noticia (AtualizadoEm, CriadoEm, IdAtualizadoPor, IdCriadoPor, StatusRegistro, Titulo, UrlImage, Fonte, Link, HoraAtras, TipoNoticia)
                                    VALUES (@AtualizadoEm, @CriadoEm, @IdAtualizadoPor, @IdCriadoPor, @StatusRegistro, @Titulo, @UrlImage, @Fonte, @Link, @HoraAtras, @TipoNoticia)
                         SELECT @@IDENTITY";

            return await _unitOfWork.Connection.ExecuteScalarAsync<long>(sql, request, _unitOfWork?.Transaction);
        }

        public async Task<Noticia> AtualizarNoticiaAsync(Noticia noticia)
        {
            var sql = @" UPDATE tbl_noticia
                            SET AtualizadoEm = @AtualizadoEm, IdAtualizadoPor = @IdAtualizadoPor, StatusRegistro = @StatusRegistro,
                                Titulo = @Titulo, UrlImage = @UrlImage, Fonte = @Fonte, Link = @Link, HoraAtras = @HoraAtras, TipoNoticia = @TipoNoticia
                            WHERE Id = @Id";

            await _unitOfWork.Connection.ExecuteAsync(sql, noticia, _unitOfWork?.Transaction);
            return noticia;
        }

        public async Task DeletarNoticiaAsync(long idNoticia)
        {
            var sql = @" UPDATE tbl_noticia
                            SET StatusRegistro = 1
                            WHERE Id = @Id";
            var obj = new
            {
                Id = idNoticia
            };

            await _unitOfWork.Connection.ExecuteAsync(sql, obj, _unitOfWork?.Transaction);

        }

        public async Task<Noticia> GetNoticiaById(long idNoticia)
        {
            var sql = @" SELECT * FROM tbl_noticia WHERE Id = @Id";
            var obj = new Noticia
            {
                Id = idNoticia
            };

            return await _unitOfWork.Connection.QueryFirstOrDefaultAsync<Noticia>(sql, obj, _unitOfWork?.Transaction);
        }
        public async Task<List<Noticia>> ListarNoticiaPorTipoAsync(TipoNoticia tipoNoticia)
        {

            var sql = @" SELECT * FROM tbl_noticia WHERE TipoNoticia = @TipoNoticia";
            var obj = new Noticia
            {
                TipoNoticia = tipoNoticia
            };

           return (List<Noticia>)await _unitOfWork.Connection.QueryAsync<Noticia>(sql, obj, _unitOfWork?.Transaction);
        }
    }
}
