using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Enums;
using Domain.Util;
using Domain.Validations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ChatService : IChatService
    {               
        private readonly UsuarioValidation _usuarioValidation;
        private readonly ChatValidation _chatValidation;
        private readonly IChatRepository _chatRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ChatService(IChatRepository chatRepository, IUsuarioRepository usuarioRepository, UsuarioValidation usuarioValidation, ChatValidation chatValidation)
        {
            _chatRepository = chatRepository;
            _usuarioRepository = usuarioRepository;
            _usuarioValidation = usuarioValidation;
            _chatValidation = chatValidation;
        }

        public async Task<long> AdicionarChat(Chat chat)
        {           
            _usuarioValidation.VerificarExistenciaUsuario(await _usuarioRepository.GetUsuarioById(chat.IdUsuarioEnvio));
            _usuarioValidation.VerificarExistenciaUsuario(await _usuarioRepository.GetUsuarioById(chat.IdUsuarioRecebe));
            
            var result = await _chatRepository.AdicionarChatAsync(chat);
            return result;
        }

        public async Task DeletarChatById(long idChat)
        {
            var errosResponse = new List<string>(0);
            var result = await _chatRepository.GetChatById(idChat);
            _chatValidation.VerificarExistenciaChat(result);
            if (result.StatusRegistro != 0)
            {
                errosResponse.Add("O chat informado já foi excluído.");
                throw new ParametroException(errosResponse);
            }
            await _chatRepository.DeletarChatAsync(idChat);
        }

        public async Task<Chat> AtualizarChat(Chat edit)
        {

            var chat = await _chatRepository.GetChatById(edit.Id);
            _chatValidation.VerificarExistenciaChat(chat);

            var response = _chatValidation.CompararChat(edit, chat);

            var result = await _chatRepository.AtualizarChatAsync(response);

            return result;
        }


        public async Task<Chat> VisualizarChatById(long idChat)
        {
            var result = await _chatRepository.GetChatById(idChat);
            _chatValidation.VerificarExistenciaChat(result);

            return result;
        }

        public async Task<List<Chat>> ListarMensagens(long idUsuarioEnvio, long idUsuarioRecebe)
        {
            _usuarioValidation.VerificarExistenciaUsuario(await _usuarioRepository.GetUsuarioById(idUsuarioEnvio));
            _usuarioValidation.VerificarExistenciaUsuario(await _usuarioRepository.GetUsuarioById(idUsuarioRecebe));
            var result = await _chatRepository.ListarMensagensAsync(idUsuarioEnvio, idUsuarioRecebe);

            return _chatValidation.ValidarListaChat(result);

        }
    }
    }
