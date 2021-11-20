using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Services;
using System.Threading.Tasks;

namespace Application.AppServices
{
    public class AvaliacaoAppService : BaseAppService
    {
        private readonly IAvaliacaoService _avaliacaoService;

        public AvaliacaoAppService(IUnitOfWork unitOfWork, IAvaliacaoService avaliacaoService) : base(unitOfWork)
        {
            _avaliacaoService = avaliacaoService;
        }

        public async Task<ResponseViewModel> AvaliarNoticia(AvaliacaoRequest request)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var result = await _avaliacaoService.AvaliarNoticia(request.IdUsuario, request.IdNoticia, request.TipoAvaliacao);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = result };
            }
        }

        public async Task<ResponseViewModel> AvaliarComentario(AvaliacaoRequest request)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var result = await _avaliacaoService.AvaliarComentario(request.IdUsuario, request.IdComentario, request.TipoAvaliacao);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = result };
            }
        }

        public async Task<ResponseViewModel> ExcluirAvaliacaoComentario(long idUsuario, long idComentario)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                await _avaliacaoService.ExcluirAvaliacaoComentario(idUsuario, idComentario);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = "O registro foi excluído com sucesso!" };
            }
        }

        public async Task<ResponseViewModel> ExcluirAvaliacaoNoticia(long idUsuario, long idNoticia)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                await _avaliacaoService.ExcluirAvaliacaoNoticia(idUsuario, idNoticia);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = "O registro foi excluído com sucesso!" };
            }
        }
    }
}
