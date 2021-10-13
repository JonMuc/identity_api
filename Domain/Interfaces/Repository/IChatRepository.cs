﻿using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IChatRepository
    {
        Task<long> AdicionarChatAsync(Chat chat);
        Task<Chat> GetChatById(long idChat);
        Task DeletarChatAsync(long idChat);
        Task<Chat> AtualizarChatAsync(Chat chat);
        Task<List<Chat>> ListarMensagensAsync(long idUsuarioEnvio, long idUsuarioRecebe);
    }
}
