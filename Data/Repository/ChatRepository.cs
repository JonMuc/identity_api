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
    public class ChatRepository : BaseRepository, IChatRepository
    {
        public ChatRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<long> AdicionarChatAsync(Chat request)
        {
            var sql = @" INSERT INTO tbl_chat (AtualizadoEm, CriadoEm, IdAtualizadoPor, IdCriadoPor, StatusRegistro, IdUsuarioEnvio, IdUsuarioRecebe, Mensagem, Leitura)
                                    VALUES (@AtualizadoEm, @CriadoEm, @IdAtualizadoPor, @IdCriadoPor, @StatusRegistro, @IdUsuarioEnvio, @IdUsuarioRecebe, @Mensagem, @Leitura)
                         SELECT @@IDENTITY";

            return await _unitOfWork.Connection.ExecuteScalarAsync<long>(sql, request, _unitOfWork?.Transaction);
        }

        public async Task<Chat> AtualizarChatAsync(Chat chat)
        {
            var sql = @" UPDATE tbl_chat
                            SET AtualizadoEm = @AtualizadoEm, IdAtualizadoPor = @IdAtualizadoPor, StatusRegistro = @StatusRegistro,
                                IdUsuarioEnvio = @IdUsuarioEnvio, IdUsuarioRecebe = @IdUsuarioRecebe, Mensagem = @Mensagem, Leitura = @Leitura
                            WHERE Id = @Id";

            await _unitOfWork.Connection.ExecuteAsync(sql, chat, _unitOfWork?.Transaction);
            return chat;
        }

        public async Task DeletarChatAsync(long idChat)
        {
            var sql = @" UPDATE tbl_chat
                            SET StatusRegistro = 1
                            WHERE Id = @Id";
            var obj = new
            {
                Id = idChat
            };

            await _unitOfWork.Connection.ExecuteAsync(sql, obj, _unitOfWork?.Transaction);

        }

        public async Task<Chat> GetChatById(long idChat)
        {
            var sql = @" SELECT * 
                            FROM tbl_chat
                            WHERE Id = @Id";
            var obj = new 
            {
                Id = idChat
            };

            return await _unitOfWork.Connection.QueryFirstOrDefaultAsync<Chat>(sql, obj, _unitOfWork?.Transaction);
        }
        public async Task<List<Chat>> ListarMensagensAsync(long idUsuarioEnvio, long idUsuarioRecebe)
        {
            var sql = @" SELECT tc.Id , tc.AtualizadoEm , tc.CriadoEm , tc.IdAtualizadoPor ,
                                tc.IdCriadoPor , tc.StatusRegistro , tc.IdUsuarioEnvio , tc.IdUsuarioRecebe ,
                                tc.Mensagem , tc.Leitura 
                            FROM TBL_CHAT tc
                            JOIN TBL_USUARIO tu 
	                            ON tc.IdUsuarioEnvio = tu.Id 
                            WHERE (tc.IdUsuarioEnvio = @idUsuarioEnvio AND tc.IdUsuarioRecebe = @idUsuarioRecebe) OR
    		                      (tc.IdUsuarioEnvio = @idUsuarioRecebe AND tc.IdUsuarioRecebe = @idUsuarioEnvio)
   	                        ORDER BY tc.CriadoEm;";

            var obj = new 
            {
                IdUsuarioEnvio = idUsuarioEnvio,
                idUsuarioRecebe = idUsuarioRecebe
            };

            return (List<Chat>)await _unitOfWork.Connection.QueryAsync<Chat>(sql, obj, _unitOfWork?.Transaction);
        }
    }
}
