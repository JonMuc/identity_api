using Domain.Interfaces;
using Domain.Models;
using Domain.Util;
using System.Collections.Generic;

namespace Domain.Validations
{
    public class ComentarioValidation
    {
        private readonly IComentarioRepository _comentarioRepository;

        public ComentarioValidation(IComentarioRepository comentarioRepository)
        {
            _comentarioRepository = comentarioRepository;
        }

        public void VerificarExistenciaComentario(Comentario comentario)
        {
            var errosResponse = new List<string>(0);

            if (comentario == null)
            {
                errosResponse.Add("O comentário informado não existe.");
                throw new ParametroException(errosResponse);
            }
        }

        public void ValidarSalvarComentarioNoticia(Comentario request)
        {
            var errosResponse = new List<string>(0);

            if (request.IdNoticia == 0)
            {
                errosResponse.Add("A 'Noticia' é obrigatória.");
            }
            if (request.IdCriadoPor == 0)
            {
                errosResponse.Add("O Id do usuário é obrigatório.");
            }
            if (errosResponse.Count > 0)
            {
                throw new ParametroException(errosResponse);
            }
        }

        public void ValidarSalvarComentarComentario(Comentario request)
        {
            var errosResponse = new List<string>(0);

            if (request.IdComentario == 0)
            {
                errosResponse.Add("A 'IdComentario' é obrigatória.");
            }
            if (request.IdCriadoPor == 0)
            {
                errosResponse.Add("O Id do usuário é obrigatório.");
            }
            if (errosResponse.Count > 0)
            {
                throw new ParametroException(errosResponse);
            }
        }

        public void ValidarExclusaoComentario(Comentario request)
        {
            var errosResponse = new List<string>(0);
            var comentario = _comentarioRepository.GetComentarioById(request.Id).Result;
            if (comentario.IdCriadoPor != request.IdCriadoPor)
            {
                errosResponse.Add("Este comentario nao pertence ao usuario logado.");
            }
            if (errosResponse.Count > 0)
            {
                throw new ParametroException(errosResponse);
            }
        }
    }
}
