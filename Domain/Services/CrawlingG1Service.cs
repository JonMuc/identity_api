using Domain.Interfaces;
using Domain.Models;
using Domain.Validations;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Net;

namespace Domain.Services
{
    public class CrawlingG1Service : ICrawlingG1Service
    {
        private readonly PessoaFisicaValidation _pessoaFisicaValidation;
        private readonly IPessoaFisicaRepository _pessoaFisicaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        private static readonly string _urlBaseGoogle = "https://g1.globo.com/";
        private static readonly string _urlBaseNoticiaGoogle = "https://news.google.com/";

        public CrawlingG1Service(PessoaFisicaValidation pessoaFisicaValidation, IPessoaFisicaRepository pessoaFisicaRepository, IUsuarioRepository usuarioRepository)
        {
            _pessoaFisicaValidation = pessoaFisicaValidation;
            _pessoaFisicaRepository = pessoaFisicaRepository;
            _usuarioRepository = usuarioRepository;
        }



        public List<Noticia> ListarManchetes()
        {
            var result = new object();
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
                var fonte = node.SelectSingleNode(".//span[contains(@class, 'feed-post-metadata-section')]");
                if (fonte != null)
                {
                    novaNoticia.Fonte = fonte.InnerText;
                }
                listaNoticias.Add(novaNoticia);
            }
            return listaNoticias;
            //return new ResponseViewModel { Sucesso = true, Objeto = listaNoticias };
        }


    }
}
