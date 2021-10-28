using Domain.Interfaces;
using Domain.Models;
using Domain.Validations;
using System;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ComentarioService : IComentarioService
    {
        private readonly ComentarioValidation _comentarioValidation;
        private readonly IComentarioRepository _comentarioRepository;

        public ComentarioService(ComentarioValidation comentarioValidation, IComentarioRepository comentarioRepository)
        {
            _comentarioValidation = comentarioValidation;
            _comentarioRepository = comentarioRepository;
        }

        public async Task<Comentario> AdicionarAsync(Comentario request)
        {
            _comentarioValidation.ValidarSalvarComentario(request);
            request.CriadoEm = DateTime.Now;
            request.Id = await _comentarioRepository.AdicionarAsync(request);
            return request;
        }

        //public async Task<Usuario> AtualizarUsuario(Usuario edit)
        //{

        //    var user = await _usuarioRepository.GetUsuarioById(edit.Id);
        //    _usuarioValidation.VerificarExistenciaUsuario(user);

        //    var usuario = _usuarioValidation.CompararUsuario(edit, user);
        //    _usuarioValidation.ValidarSalvarUsuario(usuario);

        //    var result = await _usuarioRepository.AtualizarUsuarioAsync(usuario);

        //    return result;
        //}

        //public async Task<Usuario> CriarUsuarioStep(CriarContaUsuario request)
        //{
        //    await _usuarioValidation.ValidarCriarUsuario(request);
        //    var usuario = new Usuario();
        //    usuario.Email = request.Email;
        //    usuario.Senha = CryptographyHelper.EncryptString(request.Senha);
        //    usuario.CriadoEm = DateTime.Now;
        //    usuario.Nome = request.Nome;
        //    usuario.Id = await _usuarioRepository.AdicionarUsuarioAsync(usuario);
        //    return usuario;
        //}

        //public async Task DeletarUsuarioById(long idUsuario)
        //{
        //    var result = await _usuarioRepository.GetUsuarioById(idUsuario);
        //    _usuarioValidation.VerificarExistenciaUsuario(result);

        //    await _usuarioRepository.DeletarUsuarioAsync(idUsuario);
        //}

        //public async Task<Usuario> VisualizarUsuarioById(long idUsuario)
        //{
        //    var result = await _usuarioRepository.GetUsuarioById(idUsuario);
        //    _usuarioValidation.VerificarExistenciaUsuario(result);
        //    return result;
        //}
    }
}
