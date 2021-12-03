using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Request;
using Domain.Validations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class NoticiaService : INoticiaService
    {
        private readonly NoticiaValidation _noticiaValidation;
        private readonly UsuarioValidation _usuarioValidation;
        private readonly PerfilUsuarioValidation _perfilUsuarioValidation;
        private readonly IPerfilUsuarioRepository _perfilUsuarioRepository;
        private readonly INoticiaRepository _noticiaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public NoticiaService(INoticiaRepository noticiaRepository, IUsuarioRepository usuarioRepository, 
                                NoticiaValidation noticiaValidation, UsuarioValidation usuarioValidation,
                                PerfilUsuarioValidation perfilUsuarioValidation, IPerfilUsuarioRepository perfilUsuarioRepository)
        {
            _noticiaRepository = noticiaRepository;
            _usuarioRepository = usuarioRepository;
            _perfilUsuarioRepository = perfilUsuarioRepository;
            _noticiaValidation = noticiaValidation;
            _usuarioValidation = usuarioValidation;
            _perfilUsuarioValidation = perfilUsuarioValidation;
        }

        public async Task<long> AdicionarNoticia(Noticia noticia)
        {
            var result = await _noticiaRepository.AdicionarNoticiaAsync(noticia);
            return result;
        }

        public async Task<Noticia> AtualizarNoticia(Noticia edit)
        {

            var noticia = await _noticiaRepository.GetNoticiaById(edit.Id);
            _noticiaValidation.VerificarExistenciaNoticia(noticia);

            var response = _noticiaValidation.CompararNoticia(edit, noticia);

            var result = await _noticiaRepository.AtualizarNoticiaAsync(response);

            return result;
        }

        public async Task DeletarNoticiaById(long idNoticia)
        {
            var result = await _noticiaRepository.GetNoticiaById(idNoticia);
            _noticiaValidation.VerificarExistenciaNoticia(result);

            await _noticiaRepository.DeletarNoticiaAsync(idNoticia);
        }

        public async Task<Noticia> VisualizarNoticiaById(long idNoticia)
        {
            var result = await _noticiaRepository.GetNoticiaById(idNoticia);
            _noticiaValidation.VerificarExistenciaNoticia(result);
            return result;
        }
        public async Task<IEnumerable<Noticia>> ListarNoticiaPorTipo(NoticiaRequest tipoNoticia)
        {
            var result = await _noticiaRepository.ListarNoticiaPorTipoAsync(tipoNoticia);

            return result;
        }

        public async Task<IEnumerable<Noticia>> ListarManchetes(NoticiaRequest request)
        {
            if(request.IdUsuario != 0)
            {
                _usuarioValidation.VerificarExistenciaUsuario(await _usuarioRepository.GetUsuarioById(request.IdUsuario));
                _perfilUsuarioValidation.VerificarExistenciaPerfil(await _perfilUsuarioRepository.GetPerfilUsuarioById(request.IdUsuario));
                return await _noticiaRepository.ListarManchetesAsync(request);
            }

            return await _noticiaRepository.ListarNoticiaPorTipoAsync(request);
        }

        public bool VerificarNoticiaExistente(string noticia)
        {
            string[] listaPalavras = noticia.Split(' ');
            for (var i = 0; i < listaPalavras.Length; i++)
            {
                if (i + 4 < listaPalavras.Length)
                {
                    var tituloBusca = listaPalavras[i] + " " + listaPalavras[i + 1] + " " + listaPalavras[i + 2] + " " + listaPalavras[i + 3] + " " + listaPalavras[i + 4];
                    var existe = _noticiaRepository.VerificarExistenciaTitulo("%" + tituloBusca + "%");
                    if (existe)
                    {
                        return existe;
                    }
                }
                i += 4;
            }
            return false;
        }
    }
}
