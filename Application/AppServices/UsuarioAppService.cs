using Application.ModelsDto;
using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Models.Dto;
using Domain.Models.Request;
using Domain.Services;
using System.Threading.Tasks;
using Util.Jwt;

namespace Application.AppServices
{
    public class UsuarioAppService : BaseAppService
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioAppService(IUnitOfWork unitOfWork, IUsuarioService usuarioService) : base(unitOfWork)
        {
            _usuarioService = usuarioService;
        }

        public async Task<ResponseViewModel> BuscarUsuario(string nomeUsuario, int pageIndex, int pageSize) {
            using (_unitOfWork) {
                _unitOfWork.BeginTransaction();
                var data = await _usuarioService.BuscarUsuario(nomeUsuario, pageIndex, pageSize);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = data };
            }
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
                var token = JWTManager.GenerateToken(result);
                var response = new LoginResponse
                {
                    Id = result.Id,
                    Descricao = result.Descricao,
                    Email = result.Email,
                    Foto = result.Foto,
                    Nome = result.Nome,
                    NomeUsuario = result.NomeUsuario,
                    IdGoogle = result.IdGoogle,
                    IdFacebook = result.IdFacebook,
                    PerfilInstagram = result.PerfilInstagram,
                    PerfilLinkedin = result.PerfilLinkedin,
                    TokenPush = result.TokenPush,
                    StatusRegistro = result.StatusRegistro,
                    PerfilTwitter = result.PerfilTwitter,
                    PerfilFacebook = result.PerfilFacebook,
                    Telefone = result.Telefone,
                    Token = token
                };
                return new ResponseViewModel { Sucesso = true, Objeto = response };
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

        public async Task<ResponseViewModel> VisualizarSeguidores(long idUsuario, int pageIndex, int pageSize)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var data = await _usuarioService.VisualizarSeguidores(idUsuario, pageIndex, pageSize);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = data };
            }
        }
        public async Task<ResponseViewModel> VisualizarSeguindo(long idUsuario, int pageIndex, int pageSize)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var data = await _usuarioService.VisualizarSeguindo(idUsuario, pageIndex, pageSize);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = data };
            }
        }
        public async Task<ResponseViewModel> VisualizarPerfilUsuario(int idUsuario)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var data = await _usuarioService.VisualizarPerfilUsuario(idUsuario);
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

        public async Task<ResponseViewModel> UploadImagemAsync(UploadImagemRequest request)
        {
            var response = await _usuarioService.UploadImagemAsync(request);
            return new ResponseViewModel { Sucesso = true, Objeto = response };
        }

        public async Task<ResponseViewModel> SeguirUsuario(long idUsuarioSeguido, long idUsuarioSeguidor)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var result = await _usuarioService.SeguirUsuario(idUsuarioSeguido, idUsuarioSeguidor);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = result };
            }
        }

        public async Task<ResponseViewModel> DeseguirUsuario(long idUsuarioDeseguido, long idUsuarioDeseguindo)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var result = await _usuarioService.DeseguirUsuario(idUsuarioDeseguido, idUsuarioDeseguindo);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = result };
            }
        }
    }
}
