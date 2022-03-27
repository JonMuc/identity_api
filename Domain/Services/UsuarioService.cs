using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Dto;
using Domain.Models.Request;
using Domain.Validations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Util.Criptografia;
using Util.String;

namespace Domain.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UsuarioValidation _usuarioValidation;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAwsApiService _awsApiService;

        public UsuarioService(UsuarioValidation usuarioValidation, IUsuarioRepository usuarioRepository, IAwsApiService awsApiService)
        {
            _usuarioValidation = usuarioValidation;
            _usuarioRepository = usuarioRepository;
            _awsApiService = awsApiService;
        }

        public async Task<long> SeguirUsuario(long idUsuarioSeguido, long idUsuarioSeguidor)
        {
            var entity = new Crz_SeguirUsuario();
            entity.Id = idUsuarioSeguidor;
            entity.IdUsuarioSeguido = idUsuarioSeguido;
            var result = await _usuarioRepository.SeguirUsuario(entity);
            return result;
        }

        public async Task<long> DeseguirUsuario(long idUsuarioSeguido, long idUsuarioSeguidor)
        {
            var result = await _usuarioRepository.DeseguirUsuario(idUsuarioSeguido, idUsuarioSeguidor);
            return result;
        }


        public async Task<long> AdicionarUsuario(Usuario usuario)
        {
            _usuarioValidation.ValidarSalvarUsuario(usuario);
            usuario.Senha = CryptographyHelper.EncryptString(usuario.Senha);
            var result = await _usuarioRepository.AdicionarUsuarioAsync(usuario);
            return result;
        }

        public async Task<Usuario> AtualizarUsuario(Usuario edit)
        {
            var user = await _usuarioRepository.GetUsuarioById(edit.Id);
            _usuarioValidation.VerificarExistenciaUsuario(user);

            var usuario = _usuarioValidation.CompararUsuario(edit, user);
            _usuarioValidation.ValidarSalvarUsuario(usuario);

            var result = await _usuarioRepository.AtualizarUsuarioAsync(usuario);

            return result;
        }

        public async Task<Usuario> CriarUsuarioStep(CriarContaUsuario request)
        {
            await _usuarioValidation.ValidarCriarUsuario(request);
            var usuario = new Usuario();
            usuario.Email = request.Email;
            usuario.Senha = CryptographyHelper.EncryptString(request.Senha);
            usuario.CriadoEm = DateTime.Now;
            usuario.NomeUsuario = request.NomeUsuario;
            usuario.Nome = request.Nome;
            usuario.Id = await _usuarioRepository.AdicionarUsuarioAsync(usuario);
            return usuario;
        }

        public async Task DeletarUsuarioById(long idUsuario)
        {
            var result = await _usuarioRepository.GetUsuarioById(idUsuario);
            _usuarioValidation.VerificarExistenciaUsuario(result);

            await _usuarioRepository.DeletarUsuarioAsync(idUsuario);
        }

        public async Task<Usuario> VisualizarUsuarioById(long idUsuario)
        {
            var result = await _usuarioRepository.GetUsuarioById(idUsuario);
            _usuarioValidation.VerificarExistenciaUsuario(result);
            return result;
        }

        public async Task<IEnumerable<Usuario>> BuscarUsuario(string nomeUsuario) {
            var result = await _usuarioRepository.BuscarUsuario(nomeUsuario);
            //_usuarioValidation.VerificarExistenciaUsuario(result);
            return result;
        }

        public async Task<string> UploadImagemAsync(UploadImagemRequest request)
        {
            var usuario = await _usuarioRepository.GetUsuarioById(request.IdUsuario);
            string imageName = string.Format("{0}{1}", DateTime.Now.Ticks, ".jpg");
            var path = "img-user/{0}".FormatWith(usuario.Id);
            var fullPath = "img-user/{0}-{1}".FormatWith(usuario.Id, imageName);
            _awsApiService.CreateDirectory(path);
            await _awsApiService.CreateFileAsync(fullPath, request.FileStreamIO);
            var urlImagem = "https://noticia-app.s3.amazonaws.com/" + fullPath;
            usuario.Foto = urlImagem;
            await _usuarioRepository.AtualizarUsuarioAsync(usuario);
            return urlImagem;
        }
    }
}
