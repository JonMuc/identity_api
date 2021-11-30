using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Models.Dto;
using Domain.Models.Request;
using Domain.Services;
using System.Threading.Tasks;

namespace Application.AppServices
{
    public class PerfilUsuarioAppService : BaseAppService
    {
        private readonly IPerfilUsuarioService _perfilUsuarioService;

        public PerfilUsuarioAppService(IUnitOfWork unitOfWork, IPerfilUsuarioService perfilUsuarioService) : base(unitOfWork)
        {
            _perfilUsuarioService = perfilUsuarioService;
        }

        public async Task<ResponseViewModel> AdicionarPerfil(PerfilUsuarioRequest perfilUsuario)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var result = await _perfilUsuarioService.AdicionarPerfilUsuarioAsync(perfilUsuario);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = result };
            }
        }

        public async Task<ResponseViewModel> DeletarPerfilUsuarioById(long idPerfil)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                await _perfilUsuarioService.DeletarPerfilUsuarioByIdAsync(idPerfil);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = "O registro foi excluído com sucesso!" };
            }
        }

        public async Task<ResponseViewModel> VisualizarPerfilUsuarioCompleto(long idUsuario)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var data = await _perfilUsuarioService.VisualizarPerfilUsuarioCompleto(idUsuario);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = data };
            }
        }
    }
}
