using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Services;
using System.Threading.Tasks;

namespace Application.AppServices
{
    public class ComentarioAppService : BaseAppService
    {
        private readonly IComentarioService _comentarioService;

        public ComentarioAppService(IUnitOfWork unitOfWork, IComentarioService comentarioService) : base(unitOfWork)
        {
            _comentarioService = comentarioService;
        }

        public async Task<ResponseViewModel> AdicionarAsync(Comentario request)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var response = await _comentarioService.AdicionarAsync(request);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = response };
            }
        }

        //public async Task<ResponseViewModel> AtualizarUsuario(Usuario edit)
        //{
        //    using (_unitOfWork)
        //    {
        //        _unitOfWork.BeginTransaction();
        //        var result = await _usuarioService.AtualizarUsuario(edit);
        //        _unitOfWork.CommitTransaction();
        //        return new ResponseViewModel { Sucesso = true, Objeto = result };
        //    }
        //}

        //public async Task<ResponseViewModel> DeletarUsuarioById(int idUsuario)
        //{
        //    using (_unitOfWork)
        //    {
        //        _unitOfWork.BeginTransaction();
        //        await _usuarioService.DeletarUsuarioById(idUsuario);
        //        _unitOfWork.CommitTransaction();
        //        return new ResponseViewModel { Sucesso = true, Objeto = "O registro foi excluído com sucesso!" };
        //    }
        //}
    }
}
