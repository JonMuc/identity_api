using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Enums;
using Domain.Util;
using Domain.Validations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class NoticiaService : INoticiaService
    {               
        private readonly NoticiaValidation _noticiaValidation;
        private readonly INoticiaRepository _noticiaRepository;

        public NoticiaService(INoticiaRepository noticiaRepository, NoticiaValidation noticiaValidation)
        {                      
            _noticiaRepository = noticiaRepository;
            _noticiaValidation = noticiaValidation;
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
        public async Task<List<Noticia>> ListarNoticiaPorTipo(TipoNoticia tipoNoticia)
        {
            var result = await _noticiaRepository.ListarNoticiaPorTipoAsync(tipoNoticia);
            
            return result;
        }
    }
}
