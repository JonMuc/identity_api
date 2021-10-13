using Domain.Models;
using Domain.Util;
using System.Collections.Generic;

namespace Domain.Validations
{
    public class ChatValidation
    {
        public ChatValidation()
        {
        }

        public void VerificarExistenciaChat(Chat chat)
        {
            var errosResponse = new List<string>(0);

            if (chat == null)
            {
                errosResponse.Add("O chat informado não existe.");
                throw new ParametroException(errosResponse);
            }           
        }        

        public List<Chat> ValidarListaChat(List<Chat> list)
        {
            var errosResponse = new List<string>(0);
            var listaValidada = new List<Chat>();

            if (list == null || list.Count == 0)
            {
                errosResponse.Add("Não existe mensagens trocadas entre esses usuarios.");
                throw new ParametroException(errosResponse);
            }

            foreach (Chat chat in list)
            {
                if (chat.StatusRegistro == 0)
                {
                    listaValidada.Add(chat);
                }
            }
            
            if (listaValidada == null)
            {
                errosResponse.Add("Não existem mensagens ativas para essa consulta.");
                throw new ParametroException(errosResponse);
            }
            return listaValidada;
        }

        public Chat CompararChat(Chat edit, Chat chat)
        {
            if(edit.StatusRegistro != chat.StatusRegistro)
            {
                chat.StatusRegistro = edit.StatusRegistro;
            }
            if (edit.IdUsuarioEnvio != chat.IdUsuarioEnvio)
            {
                chat.IdUsuarioEnvio = edit.IdUsuarioEnvio;
            }
            if (edit.IdUsuarioRecebe != chat.IdUsuarioRecebe) 
            {
                chat.IdUsuarioRecebe = edit.IdUsuarioRecebe;
            }
            if (edit.Mensagem != chat.Mensagem) 
            {
                chat.Mensagem = edit.Mensagem;
            }
            if (edit.Leitura != chat.Leitura)
            {
                chat.Leitura = edit.Leitura;
            }

            chat.AtualizadoEm = System.DateTime.Now;
            //chat.IdAtualizadoPor = Id do usuario

            return chat;
        }

    }
}
