using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Enums;
using Domain.Util;
using Domain.Validations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class NoticiaFavoritoService : INoticiaFavoritoService
    {               
        private readonly NoticiaFavoritoValidation _noticiaFavoritoValidation;
        private readonly NoticiaValidation _noticiaValidation;
        private readonly UsuarioValidation _usuarioValidation;
        private readonly INoticiaFavoritoRepository _noticiaFavoritoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly INoticiaRepository _noticiaRepository;

        public NoticiaFavoritoService(INoticiaFavoritoRepository noticiaFavoritoRepository, NoticiaFavoritoValidation noticiaFavoritoValidation,
            INoticiaRepository noticiaRepository, IUsuarioRepository usuarioRepository, NoticiaValidation noticiaValidation, UsuarioValidation usuarioValidation)
        {
            _noticiaFavoritoRepository = noticiaFavoritoRepository;
            _noticiaFavoritoValidation = noticiaFavoritoValidation;
            _noticiaValidation = noticiaValidation;
            _usuarioValidation = usuarioValidation;
            _usuarioRepository = usuarioRepository;
            _noticiaRepository = noticiaRepository;
        }

        public async Task<long> AdicionarNoticiaFavorito(NoticiaFavorito noticiaFavorito)
        {           
            _usuarioValidation.VerificarExistenciaUsuario(await _usuarioRepository.GetUsuarioById(noticiaFavorito.IdUsuario));
            _noticiaValidation.VerificarExistenciaNoticia(await _noticiaRepository.GetNoticiaById(noticiaFavorito.IdNoticia));
            var result = await _noticiaFavoritoRepository.AdicionarNoticiaFavoritoAsync(noticiaFavorito);
            return result;
        }

        public async Task<NoticiaFavorito> AtualizarNoticiaFavorito(NoticiaFavorito edit)
        {

            var noticiaFavorito = await _noticiaFavoritoRepository.GetNoticiaFavoritoById(edit.Id);
            _noticiaFavoritoValidation.VerificarExistenciaNoticiaFavorito(noticiaFavorito);

            var response = _noticiaFavoritoValidation.CompararNoticiaFavorito(edit, noticiaFavorito);

            var result = await _noticiaFavoritoRepository.AtualizarNoticiaFavoritoAsync(response);

            return result;
        }

        public async Task DeletarNoticiaFavoritoById(long idNoticiaFavorito)
        {
            var result = await _noticiaFavoritoRepository.GetNoticiaFavoritoById(idNoticiaFavorito);
            _noticiaFavoritoValidation.VerificarExistenciaNoticiaFavorito(result);

            await _noticiaFavoritoRepository.DeletarNoticiaFavoritoAsync(idNoticiaFavorito);
        }

        public async Task<NoticiaFavorito> VisualizarNoticiaFavoritoById(long idNoticia)
        {
            var result = await _noticiaFavoritoRepository.GetNoticiaFavoritoById(idNoticia);
            _noticiaFavoritoValidation.VerificarExistenciaNoticiaFavorito(result);
            result.Noticia = await _noticiaRepository.GetNoticiaById(result.IdNoticia);
            result.Usuario = await _usuarioRepository.GetUsuarioById(result.IdUsuario);             
            return result;
        }
        //public async Task<List<Noticia>> ListarNoticiaPorTipo(TipoNoticia tipoNoticia)
        //{
        //    var result = await _noticiaRepository.ListarNoticiaPorTipoAsync(tipoNoticia);
            
        //    return result;
        //}
    }
}
