using Domain.Interfaces;
using Domain.Services;
using Domain.Models;
using Domain.Models.Enums;
using Domain.Services;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;

namespace Jobs.Job
{
    public class ImportarNoticiasG1EconomiaJob : BaseJob
    {
        protected readonly INoticiaRepository _noticiaRepository;
        protected readonly INoticiaService _noticiaService;

        private static readonly string _urlBaseG1 = "https://g1.globo.com/economia/";

        public ImportarNoticiasG1EconomiaJob(INoticiaRepository noticiaRepository, INoticiaService noticiaService)
        {
            _noticiaRepository = noticiaRepository;
            _noticiaService = noticiaService;
        }


        public override void Process()
        {
            ProcessarNoticias();
        }

        protected override void Init()
        {
        }

        private void ProcessarNoticias()
        {
            string url = _urlBaseG1;
            string html;

            var htmlDoc = new HtmlDocument();

            using (WebClient wc = new WebClient())
            {
                wc.Headers["Cookie"] = "security=true";
                html = wc.DownloadString(url);
            }


            htmlDoc.LoadHtml(html);
            // pegando uma lista com as tabelas da página
            var todasAsTabelas = htmlDoc.DocumentNode.SelectNodes(".//div[contains(@class, 'bstn-err-container')]");

            var listaNoticias = new List<Noticia>();

            foreach (HtmlNode node in todasAsTabelas)
            {
                var novaNoticia = new Noticia();
                novaNoticia.Titulo = node.SelectSingleNode(".//div//div//div//div[2]//div//a").InnerText;
                novaNoticia.Link = node.SelectSingleNode(".//div//div//div//div[2]//div//a").GetAttributeValue("href", string.Empty);
                novaNoticia.HoraAtras = node.SelectSingleNode(".//span[contains(@class, 'feed-post-datetime')]").InnerText;
                var UrlImage = node.SelectSingleNode(".//img[contains(@class, 'bstn-fd-picture-image')]");
                if (UrlImage != null)
                {
                    novaNoticia.UrlImage = UrlImage.GetAttributeValue("src", string.Empty);
                    novaNoticia.UrlImage.Replace("w100-h100", "w500-h500");
                }
                //var fonte = node.SelectSingleNode(".//span[contains(@class, 'feed-post-metadata-section')]");
                //if (fonte != null)
                //{
                //    novaNoticia.Fonte = fonte.InnerText.Substring(1);
                //}
                listaNoticias.Add(novaNoticia);
            }            
            SalvarNoticias(listaNoticias);
        }

        private void SalvarNoticias(List<Noticia> listaNoticias)
        {
            foreach (Noticia noticia in listaNoticias)
            {
                if(noticia.UrlImage != null)
                {
                    if (!_noticiaRepository.VerificarExistenciaTitulo(noticia.Titulo))
                    {
                        noticia.OrigemNoticia = OrigemNoticia.G1;
                        noticia.TipoNoticia = TipoNoticia.Negocios;
                        noticia.CriadoEm = DateTime.Now;
                        _noticiaRepository.AdicionarNoticia(noticia);
                    }
                }               
            }
        }
    }
}
