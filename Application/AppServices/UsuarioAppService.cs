using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Models.Dto;
using Domain.Services;
using System.Threading.Tasks;

namespace Application.AppServices
{
    public class UsuarioAppService : BaseAppService
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioAppService(IUnitOfWork unitOfWork, IUsuarioService usuarioService) : base(unitOfWork)
        {
            _usuarioService = usuarioService;
        }

        public async Task<ResponseViewModel> AdicionarUsuario(Usuario usuario)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var result = await _usuarioService.AdicionarUsuario(usuario);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = result };
            }
        }

        public async Task<ResponseViewModel> AtualizarUsuario(Usuario edit)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var result = await _usuarioService.AtualizarUsuario(edit);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = result };
            }
        }

        public async Task<ResponseViewModel> DeletarUsuarioById(int idUsuario)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                await _usuarioService.DeletarUsuarioById(idUsuario);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = "O registro foi excluído com sucesso!" };
            }
        }

        public async Task<ResponseViewModel> VisualizarUsuarioById(int idUsuario)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var data = await _usuarioService.VisualizarUsuarioById(idUsuario);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = data };
            }
        }

        public async Task<ResponseViewModel> CriarUsuarioStep(CriarContaUsuario request)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var response = await _usuarioService.CriarUsuarioStep(request);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = response };
            }
        }
    }
}
