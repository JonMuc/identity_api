using Domain.Models;
using Domain.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IChatService
    {
        Task<long> AdicionarChat(Chat chat);
        Task DeletarChatById(long idChat);
        Task<Chat> AtualizarChat(Chat chat);
        Task<Chat> VisualizarChatById(long idChat);
        Task<List<Chat>> ListarMensagens(long idUsuarioEnvio, long idUsuarioRecebe);
    }
}
