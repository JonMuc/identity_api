using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Services;
using System.Threading.Tasks;

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

        public async Task<ResponseViewModel> ListarComentariosNoticiaAsync(long idNoticia)
        {
            var response = await _comentarioRepository.ListarComentariosNoticiaAsync(idNoticia);
            return new ResponseViewModel { Sucesso = true, Objeto = response };
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
