using Dapper;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class NoticiaRepository : BaseRepository, INoticiaRepository
    {
        public NoticiaRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public long AdicionarNoticia(Noticia request)
        {
            var sql = @" INSERT INTO tbl_noticia (AtualizadoEm, CriadoEm, IdAtualizadoPor, IdCriadoPor, StatusRegistro, Titulo, UrlImage, Fonte, Link, HoraAtras, TipoNoticia, OrigemNoticia )
                                    VALUES (@AtualizadoEm, @CriadoEm, @IdAtualizadoPor, @IdCriadoPor, @StatusRegistro, @Titulo, @UrlImage, @Fonte, @Link, @HoraAtras, @TipoNoticia, @OrigemNoticia)
                         SELECT @@IDENTITY";
            var response = _unitOfWork.Connection.ExecuteScalar<long>(sql, request, _unitOfWork?.Transaction);
            return response;
        }


        public async Task<long> AdicionarNoticiaAsync(Noticia request)
        {
            var sql = @" INSERT INTO tbl_noticia (AtualizadoEm, CriadoEm, IdAtualizadoPor, IdCriadoPor, StatusRegistro, Titulo, UrlImage, Fonte, Link, HoraAtras, TipoNoticia)
                                    VALUES (@AtualizadoEm, @CriadoEm, @IdAtualizadoPor, @IdCriadoPor, @StatusRegistro, @Titulo, @UrlImage, @Fonte, @Link, @HoraAtras, @TipoNoticia)
                         SELECT @@IDENTITY";
            var response = await _unitOfWork.Connection.ExecuteScalarAsync<long>(sql, request, _unitOfWork?.Transaction);
            return response;
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

        //SELECT * FROM TBL_NOTICIA ORDER BY Id asc OFFSET 20 ROWS FETCH NEXT  10  ROWS ONLY
        public async Task<IEnumerable<Noticia>> ListarNoticiaPorTipoAsync(NoticiaRequest request)
        {
            //request.PageIndex = request.PageIndex * request.PageSize;
            var TipoNoticia = (int)request.TipoNoticia;
            var sql = @" SELECT * FROM tbl_noticia WHERE TipoNoticia = @TipoNoticia ORDER BY Id asc OFFSET @PageIndex ROWS FETCH NEXT  @PageSize  ROWS ONLY";
            var response = await _unitOfWork.Connection.QueryAsync<Noticia>(sql, new { request.PageSize, request.PageIndex, TipoNoticia }, _unitOfWork?.Transaction);
            return response;
        }

        public async Task<IEnumerable<Noticia>> ListarNoticiaAsync(NoticiaRequest request)
        {
            //var sql = @" SELECT * FROM tbl_noticia ORDER BY Id asc OFFSET " + request.PageSize * request.PageIndex + " ROWS FETCH NEXT  " + request.PageSize + "  ROWS ONLY";
            var sql = @" SELECT * FROM tbl_noticia ORDER BY Id asc OFFSET @PageIndex ROWS FETCH NEXT  @PageSize  ROWS ONLY";

            var response = await _unitOfWork.Connection.QueryAsync<Noticia>(sql, request, _unitOfWork?.Transaction);
            return response;
        }

        public async Task<bool> VerificarExistenciaTituloAsync(string titulo)
        {
            var sql = @"SELECT count(*) FROM tbl_noticia WHERE Titulo like @titulo";
            var response = await _unitOfWork.Connection.ExecuteScalarAsync<bool>(sql, new { titulo }, _unitOfWork?.Transaction);
            return response;
        }

        public bool VerificarExistenciaTitulo(string titulo)
        {
            var sql = @"SELECT count(*) FROM tbl_noticia WHERE Titulo like @titulo";
            var response = _unitOfWork.Connection.ExecuteScalar<bool>(sql, new { titulo }, _unitOfWork?.Transaction);
            return response;
        }
    }
}
