using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Enums;
using Domain.Services;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;

namespace Jobs.Job
{
    public class ImportarNoticiasGoogleNegociosJob : BaseJob
    {
        protected readonly INoticiaRepository _noticiaRepository;
        protected readonly INoticiaService _noticiaService;

        private static readonly string _urlBaseGoogle = "https://news.google.com/topics/CAAqKggKIiRDQkFTRlFvSUwyMHZNRGx6TVdZU0JYQjBMVUpTR2dKQ1VpZ0FQAQ?hl=pt-BR&gl=BR&ceid=BR%3Apt-419";
        private static readonly string _urlBaseNoticiaGoogle = "https://news.google.com";

        public ImportarNoticiasGoogleNegociosJob(INoticiaRepository noticiaRepository, INoticiaService noticiaService)
        {
            _noticiaRepository = noticiaRepository;
            _noticiaService = noticiaService;
        }


        public override void Process()
        {
            ProcessarNoticias();
        }

        protected override void Init() { }

        private void ProcessarNoticias()
        {
            string url = _urlBaseGoogle;
            string html;

            var htmlDoc = new HtmlDocument();

            using (WebClient wc = new WebClient())
            {
                wc.Headers["Cookie"] = "security=true";
                html = wc.DownloadString(url);
            }


            htmlDoc.LoadHtml(html);
            // pegando uma lista com as tabelas da página
            var todasAsTabelas = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'DBQmFf NclIid BL5WZb Oc0wGc xP6mwf j7vNaf')]");

            var listaNoticias = new List<Noticia>();

            foreach (HtmlNode node in todasAsTabelas)
            {
                var novaNoticia = new Noticia();
                novaNoticia.Titulo = node.SelectSingleNode(".//div//article//h3//a").InnerText;
                novaNoticia.Link = _urlBaseNoticiaGoogle + node.SelectSingleNode(".//div//article//h3//a").GetAttributeValue("href", string.Empty).Remove(0, 1);
                novaNoticia.Fonte = node.SelectSingleNode(".//div//article//div//div//a").InnerText;
                novaNoticia.HoraAtras = node.SelectSingleNode(".//div//article//div//div//time").InnerText;
                novaNoticia.UrlImage = node.SelectSingleNode(".//div//a//figure//img").GetAttributeValue("src", string.Empty);
                listaNoticias.Add(novaNoticia);
            }
            SalvarNoticias(listaNoticias);
        }

        private void SalvarNoticias(List<Noticia> listaNoticias)
        {
            foreach (Noticia noticia in listaNoticias)
            {
                if (noticia.UrlImage != null)
                {
                    if (!_noticiaRepository.VerificarExistenciaTitulo(noticia.Titulo))
                    {
                        noticia.OrigemNoticia = OrigemNoticia.GoogleNews;
                        noticia.TipoNoticia = TipoNoticia.Negocios;
                        noticia.CriadoEm = DateTime.Now;
                        _noticiaRepository.AdicionarNoticia(noticia);
                    }
                }                
            }
        }
    }
}
