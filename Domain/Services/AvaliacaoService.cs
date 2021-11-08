using Domain.Interfaces;
using Domain.Models;
using Domain.Validations;
using System;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class AvaliacaoService : IAvaliacaoService
    {
        private readonly ComentarioValidation _comentarioValidation;
        private readonly IComentarioRepository _comentarioRepository;

        public AvaliacaoService(ComentarioValidation comentarioValidation, IComentarioRepository comentarioRepository)
        {
            _comentarioValidation = comentarioValidation;
            _comentarioRepository = comentarioRepository;
        }

        public async Task<Comentario> AdicionarAsync(Comentario request)
        {
            _comentarioValidation.ValidarSalvarComentario(request);
            request.CriadoEm = DateTime.Now;
            request.Id = await _comentarioRepository.AdicionarAsync(request);
            return request;
        }
    }
}
