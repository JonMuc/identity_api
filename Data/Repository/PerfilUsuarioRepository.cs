using Dapper;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Models.Dto;
using Domain.Models.Enums;
using Domain.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class PerfilUsuarioRepository : BaseRepository, IPerfilUsuarioRepository
    {
        public PerfilUsuarioRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<long> AdicionarPerfilUsuarioAsync(PerfilUsuario perfilUsuario)
        {
            var sql = @" INSERT INTO tbl_perfil_usuario (AtualizadoEm, CriadoEm, IdAtualizadoPor, IdCriadoPor, StatusRegistro, IdUsuario, TipoNoticia)
                                    VALUES (@AtualizadoEm, @CriadoEm, @IdAtualizadoPor, @IdCriadoPor, @StatusRegistro, @IdUsuario, @TipoNoticia)
                         SELECT @@IDENTITY";
            var response = await _unitOfWork.Connection.ExecuteScalarAsync<long>(sql, perfilUsuario, _unitOfWork?.Transaction);
            return response;
        }       

        public async Task DeletarPerfilUsuarioAsync(long idPerfil)
        {
            var sql = @" UPDATE tbl_perfil_usuario
                            SET StatusRegistro = 1,
                                AtualizadoEm = GETDATE()
                            WHERE Id = @Id";            

            await _unitOfWork.Connection.ExecuteAsync(sql, new { Id = idPerfil }, _unitOfWork?.Transaction);
        }

        public async Task<PerfilUsuario> GetPerfilUsuarioById(long idUsuario)
        {
            var sql = @" SELECT * FROM tbl_perfil_usuario WHERE IdUsuario = @Id AND StatusRegistro = 0";            

            return await _unitOfWork.Connection.QueryFirstOrDefaultAsync<PerfilUsuario>(sql, new { Id = idUsuario }, _unitOfWork?.Transaction);
        }

        public async Task<long> VerificarPerfilUsuarioAsync(PerfilUsuarioRequest request)
        {
            var sql = @" SELECT Id FROM tbl_perfil_usuario 
                            WHERE IdUsuario = @IdUsuario AND TipoNoticia = @TipoNoticia AND StatusRegistro = 0";

            return await _unitOfWork.Connection.QueryFirstOrDefaultAsync<long>(sql, request, _unitOfWork?.Transaction);
        }
        public async Task<IEnumerable<TipoNoticia>> VisualizarPerfilUsuarioCompletoAsync(long idUsuario)
        {
            var sql = @" SELECT pfu.TipoNoticia
                            FROM tbl_perfil_usuario pfu
                            WHERE IdUsuario = @IdUsuario AND StatusRegistro = 0";

            return await _unitOfWork.Connection.QueryAsync<TipoNoticia>(sql, new { IdUsuario = idUsuario }, _unitOfWork?.Transaction);
        }
    }
}
