using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Models.Request;
using Domain.Services;
using System.Threading.Tasks;

namespace Application.AppServices
{
    public class NoticiaAppService : BaseAppService
    {
        private readonly ICrawlingGoogleService _crawlingGoogleService;
        private readonly ICrawlingG1Service _crawlingG1Service;
        private readonly INoticiaService _noticiaService;
        private readonly INoticiaRepository _noticiaRepository;

        public NoticiaAppService(IUnitOfWork unitOfWork, ICrawlingGoogleService crawlingGoogleService, ICrawlingG1Service crawlingG1Service, INoticiaService noticiaService, INoticiaRepository noticiaRepository) : base(unitOfWork)
        {
            _crawlingGoogleService = crawlingGoogleService;
            _crawlingG1Service = crawlingG1Service;
            _noticiaService = noticiaService;
            _noticiaRepository = noticiaRepository;
        }

        public async Task<ResponseViewModel> ListarManchete(NoticiaRequest request)
        {
            //var listaNoticia = new List<Noticia>();
            var listaNoticia = await _noticiaRepository.ListarNoticiaAsync(request);
            //listaNoticia = _crawlingGoogleService.ListarManchetes();
            //var g1 = _crawlingG1Service.ListarManchetes();
            //listaNoticia.AddRange(g1);
            var result = listaNoticia;
            return new ResponseViewModel { Sucesso = true, Objeto = result };
        }

        public ResponseViewModel ListarMancheteG1()
        {
            var result = _crawlingG1Service.ListarManchetes();
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

        public async Task<ResponseViewModel> VisualizarNoticiaById(int idNoticia)
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
