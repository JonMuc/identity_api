using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Services;
using System.Threading.Tasks;

namespace Application.AppServices
{
    public class ComentarioAppService : BaseAppService
    {
        private readonly IComentarioService _comentarioService;
        private readonly IComentarioRepository _comentarioRepository;

        public ComentarioAppService(IUnitOfWork unitOfWork, IComentarioService comentarioService, IComentarioRepository comentarioRepository) : base(unitOfWork)
        {
            _comentarioService = comentarioService;
            _comentarioRepository = comentarioRepository;
        }

        public async Task<ResponseViewModel> SalvarComentarioNoticiaAsync(Comentario request)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                await _comentarioService.AdicionarComentarioNoticiaAsync(request);
                var response = await _comentarioRepository.ListarComentariosNoticiaAsync(request.IdNoticia);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = response };
            }
        }
        public async Task<ResponseViewModel> ComentarComentarioAsync(Comentario request)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                await _comentarioService.ComentarComentarioAsync(request);
                var response = await _comentarioRepository.ListarComentariosComentarioAsync(request.IdComentario);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = response };
            }
        }

        public async Task<ResponseViewModel> ListarComentariosNoticiaAsync(long idNoticia)
        {
            var response = await _comentarioRepository.ListarComentariosNoticiaAsync(idNoticia);
            return new ResponseViewModel { Sucesso = true, Objeto = response };
        }

        public async Task<ResponseViewModel> ListarComentariosComentarioAsync(long idComentario)
        {
            var response = await _comentarioRepository.ListarComentariosNoticiaAsync(idComentario);
            return new ResponseViewModel { Sucesso = true, Objeto = response };
        }
    }
}
