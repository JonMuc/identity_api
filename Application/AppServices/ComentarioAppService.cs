using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Models.Request;
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

        public async Task<ResponseViewModel> ExcluirComentarioNoticiaAsync(Comentario request)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                await _comentarioService.ExcluirComentarioNoticiaAsync(request);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = request };
            }
        }

        public async Task<ResponseViewModel> SalvarComentarioNoticiaAsync(Comentario request)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                await _comentarioService.AdicionarComentarioNoticiaAsync(request);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = request };
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

        public async Task<ResponseViewModel> ListarComentariosNoticiaAsync(ComentarioRequest request)
        {
            var response = await _comentarioRepository.ListarComentariosNoticiaAsync(request);
            return new ResponseViewModel { Sucesso = true, Objeto = response };
        }

        public async Task<ResponseViewModel> ListarComentariosNoticiaDeslogadoAsync(ComentarioRequest request)
        {
            var response = await _comentarioRepository.ListarComentariosNoticiaDeslogadoAsync(request);
            return new ResponseViewModel { Sucesso = true, Objeto = response };
        }

        public async Task<ResponseViewModel> ListarComentariosComentarioAsync(ComentarioRequest request)
        {            
            var response = await _comentarioRepository.ListarComentariosComentarioAsync(request);
            return new ResponseViewModel { Sucesso = true, Objeto = response };            
        }

        public async Task<ResponseViewModel> ListarComentariosComentarioDeslogadoAsync(int idComentario)
        {
            var response = await _comentarioRepository.ListarComentariosComentarioAsync(new ComentarioRequest { IdComentario = idComentario });
            return new ResponseViewModel { Sucesso = true, Objeto = response };
        }
    }
}
