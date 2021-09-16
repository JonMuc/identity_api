using Domain.Interfaces.Repository;
using Domain.Models;
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

    }
}
