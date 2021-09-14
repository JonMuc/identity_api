using Domain.Models;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.AppServices
{
    public class UsuarioAppService
    {
        private readonly ICrawlingGoogleService _crawlingGoogleService;
        private readonly ICrawlingG1Service _crawlingG1Service;

        public UsuarioAppService(ICrawlingGoogleService crawlingGoogleService, ICrawlingG1Service crawlingG1Service)
        {
            _crawlingGoogleService = crawlingGoogleService;
            _crawlingG1Service = crawlingG1Service;
        }

        public ResponseViewModel ListarManchete()
        {
            var listaNoticia = new List<Noticia>();

            listaNoticia = _crawlingGoogleService.ListarManchetes();
            var g1 = _crawlingG1Service.ListarManchetes();
            listaNoticia.AddRange(g1);
            var result = listaNoticia.OrderBy(elem => Guid.NewGuid());
            return new ResponseViewModel { Sucesso = true, Objeto = result };
        }

        public ResponseViewModel ListarMancheteG1()
        {
            var result = _crawlingG1Service.ListarManchetes();
            return new ResponseViewModel { Sucesso = true, Objeto = result };
        }
    }
}
