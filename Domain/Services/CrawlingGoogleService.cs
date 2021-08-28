using Domain.Interfaces;
using Domain.Models;
using Domain.Validations;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Net;

namespace Domain.Services
{
    public class CrawlingGoogleService : ICrawlingGoogleService
    {
        private readonly PessoaFisicaValidation _pessoaFisicaValidation;
        private readonly IPessoaFisicaRepository _pessoaFisicaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        private static readonly string _urlBaseGoogle = "https://news.google.com/topstories?hl=pt-BR&gl=BR&ceid=BR:pt-419";
        private static readonly string _urlBaseNoticiaGoogle = "https://news.google.com";

        public CrawlingGoogleService(PessoaFisicaValidation pessoaFisicaValidation, IPessoaFisicaRepository pessoaFisicaRepository, IUsuarioRepository usuarioRepository)
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
            var todasAsTabelas = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'DBQmFf NclIid BL5WZb Oc0wGc xP6mwf j7vNaf')]");

            var listaNoticias = new List<Noticia>();

            foreach (HtmlNode node in todasAsTabelas)
            {
                var novaNoticia = new Noticia();
                novaNoticia.Titulo = node.SelectSingleNode(".//div//article//h3//a").InnerText;
                novaNoticia.Link = _urlBaseNoticiaGoogle + node.SelectSingleNode(".//div//article//h3//a").GetAttributeValue("href", string.Empty).Remove(0, 1);
                novaNoticia.Fonte = node.SelectSingleNode(".//div//article//div//div//a").InnerText;
                novaNoticia.HoraAtras = node.SelectSingleNode(".//div//article//div//div//time").InnerText;
                novaNoticia.UrlImage = node.SelectSingleNode(".//div//a//figure//img").GetAttributeValue("src", string.Empty).Replace("w100-h100", "h500-w500");
                listaNoticias.Add(novaNoticia);
            }
            return listaNoticias;
            //return new ResponseViewModel { Sucesso = true, Objeto = listaNoticias };
        }


    }
}
