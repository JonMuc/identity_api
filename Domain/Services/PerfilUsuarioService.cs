using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Enums;
using Domain.Models.Request;
using Domain.Validations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class PerfilUsuarioService : IPerfilUsuarioService
    {
        private readonly IPerfilUsuarioRepository _perfilUsuarioRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly PerfilUsuarioValidation _perfilUsuarioValidation;
        private readonly UsuarioValidation _usuarioValidation;

        public PerfilUsuarioService(IPerfilUsuarioRepository perfilUsuarioRepository, PerfilUsuarioValidation perfilUsuarioValidation,
                                    IUsuarioRepository usuarioRepository, UsuarioValidation usuarioValidation)
        {
            _perfilUsuarioRepository = perfilUsuarioRepository;
            _usuarioRepository = usuarioRepository;
            _perfilUsuarioValidation = perfilUsuarioValidation;
            _usuarioValidation = usuarioValidation;
        }

        public async Task<long> AdicionarPerfilUsuarioAsync(PerfilUsuarioRequest request)
        {
            //existe o usuario informado?
            _usuarioValidation.VerificarExistenciaUsuario(await _usuarioRepository.GetUsuarioById(request.IdUsuario));
            //o usuario informado já possui esse interesse ativo?
            var check = await _perfilUsuarioRepository.VerificarPerfilUsuarioAsync(request);
            //o tipo de noticia passado é um tipo valido?
            _perfilUsuarioValidation.ValidarInclusaoPerfilUsuario(check, request.TipoNoticia);

            var obj = new PerfilUsuario().toModel(request);
            obj.CriadoEm = DateTime.Now;

            return await _perfilUsuarioRepository.AdicionarPerfilUsuarioAsync(obj);            
        }

        public async Task DeletarPerfilUsuarioByIdAsync(long idUsuario)
        {            
            _perfilUsuarioValidation.VerificarExistenciaPerfil(await _perfilUsuarioRepository.GetPerfilUsuarioById(idUsuario));

            await _perfilUsuarioRepository.DeletarPerfilUsuarioAsync(idUsuario);
        }

        public async Task<IEnumerable<TipoNoticia>> VisualizarPerfilUsuarioCompleto(long idUsuario)
        {
            //existe o usuario informado?
            _usuarioValidation.VerificarExistenciaUsuario(await _usuarioRepository.GetUsuarioById(idUsuario));

            var list = await _perfilUsuarioRepository.VisualizarPerfilUsuarioCompletoAsync(idUsuario);
            _perfilUsuarioValidation.ValidarPerfil(list);
            return list;            
        }

    }
}
