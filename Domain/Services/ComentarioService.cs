using Domain.Interfaces;
using Domain.Models;
using Domain.Validations;
using System;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ComentarioService : IComentarioService
    {
        private readonly ComentarioValidation _comentarioValidation;
        private readonly IComentarioRepository _comentarioRepository;

        public ComentarioService(ComentarioValidation comentarioValidation, IComentarioRepository comentarioRepository)
        {
            _comentarioValidation = comentarioValidation;
            _comentarioRepository = comentarioRepository;
        }

        public async Task ExcluirComentarioNoticiaAsync(Comentario request)
        {
            _comentarioValidation.ValidarExclusaoComentario(request);
            request.AtualizadoEm = DateTime.Now;
            await _comentarioRepository.ExcluirAsync(request);
        }

        public async Task<Comentario> AdicionarComentarioNoticiaAsync(Comentario request)
        {
            _comentarioValidation.ValidarSalvarComentarioNoticia(request);
            request.CriadoEm = DateTime.Now;
            request.Id = await _comentarioRepository.AdicionarAsync(request);
            return request;
        }

        public async Task<Comentario> ComentarComentarioAsync(Comentario request)
        {
            _comentarioValidation.ValidarSalvarComentarComentario(request);
            request.CriadoEm = DateTime.Now;
            request.Id = await _comentarioRepository.AdicionarAsync(request);
            return request;
        }
    }
}
