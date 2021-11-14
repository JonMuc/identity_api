using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Services;

namespace Application.AppServices
{
    public class AvaliacaoAppService : BaseAppService
    {
        private readonly IComentarioService _comentarioService;
        private readonly IComentarioRepository _comentarioRepository;

        public AvaliacaoAppService(IUnitOfWork unitOfWork, IComentarioService comentarioService, IComentarioRepository comentarioRepository) : base(unitOfWork)
        {
            _comentarioService = comentarioService;
            _comentarioRepository = comentarioRepository;
        }

    }
}
