using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Models.Request;
using Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.AppServices
{
    public class NoticiaAppService : BaseAppService
    {
        private readonly ICrawlingG1Service _crawlingG1Service;
        private readonly INoticiaService _noticiaService;
        private readonly INoticiaRepository _noticiaRepository;

        public NoticiaAppService(IUnitOfWork unitOfWork, ICrawlingGoogleService crawlingGoogleService, ICrawlingG1Service crawlingG1Service, INoticiaService noticiaService, INoticiaRepository noticiaRepository) : base(unitOfWork)
        {
            _crawlingG1Service = crawlingG1Service;
            _noticiaService = noticiaService;
            _noticiaRepository = noticiaRepository;
        }

        public async Task<ResponseViewModel> ListarManchete(NoticiaRequest request)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var result = await _noticiaService.ListarManchetes(request);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = result };
            }
        }

        public async Task<ResponseViewModel> ListarNoticias(NoticiaRequest request)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var result = await _noticiaRepository.ListarNoticias(request);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = result };
            }
        }

        public ResponseViewModel ListarMancheteG1()
        {
            var result = _noticiaRepository.ListarManchetesTemp();
            foreach (Noticia noticia in result)
            {
                noticia.UrlImage = noticia.UrlImage.Replace("w100-h100", "w1000-h1000");
            }
            return new ResponseViewModel { Sucesso = true, Objeto = result };
        }

        public async Task<ResponseViewModel> AdicionarNoticia(Noticia noticia)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var result = await _noticiaService.AdicionarNoticia(noticia);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = result };
            }
        }

        public async Task<ResponseViewModel> AtualizarNoticia(Noticia edit)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var result = await _noticiaService.AtualizarNoticia(edit);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = result };
            }
        }

        public async Task<ResponseViewModel> DeletarNoticiaById(int idNoticia)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                await _noticiaService.DeletarNoticiaById(idNoticia);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = "O registro foi excluído com sucesso!" };
            }
        }

        public async Task<ResponseViewModel> VisualizarNoticiaById(long idNoticia)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var data = await _noticiaService.VisualizarNoticiaById(idNoticia);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = data };
            }
        }

        public async Task<ResponseViewModel> ListarNoticiaPorTipo(NoticiaRequest tipoNoticia)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var data = await _noticiaService.ListarNoticiaPorTipo(tipoNoticia);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = data };
            }
        }
    }
}
