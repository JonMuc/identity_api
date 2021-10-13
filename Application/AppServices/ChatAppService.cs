using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Models.Enums;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.AppServices
{
    public class ChatAppService : BaseAppService
    {        
        private readonly IChatService _chatService;

        public ChatAppService(IUnitOfWork unitOfWork, IChatService chatService) : base(unitOfWork)
        {
            _chatService = chatService;
        }
        public async Task<ResponseViewModel> AdicionarChat(Chat chat)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var result = await _chatService.AdicionarChat(chat);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = result };
            }
        }

        public async Task<ResponseViewModel> DeletarChatById(int idChat)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                await _chatService.DeletarChatById(idChat);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = "O registro foi excluído com sucesso!" };
            }
        }


        public async Task<ResponseViewModel> AtualizarChat(Chat edit)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var result = await _chatService.AtualizarChat(edit);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = result };
            }
        }

        public async Task<ResponseViewModel> VisualizarChatById(int idChat)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var data = await _chatService.VisualizarChatById(idChat);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = data };
            }
        }

        public async Task<ResponseViewModel> ListarMensagens(long idUsuarioEnvio, long idUsuarioRecebe)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var data = await _chatService.ListarMensagens(idUsuarioEnvio, idUsuarioRecebe);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = data };
            }
        }
    }
}
