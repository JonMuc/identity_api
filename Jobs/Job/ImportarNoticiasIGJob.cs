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
    public class ImportarNoticiasIGJob : BaseJob
    {
        protected readonly INoticiaRepository _noticiaRepository;
        protected readonly INoticiaService _noticiaService;

        private static readonly string _urlBaseIG = "https://www.ig.com.br";

        public ImportarNoticiasIGJob(INoticiaRepository noticiaRepository, INoticiaService noticiaService)
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

        private void ProcessarNoticias() //Nao tem como buscar Noticia.HorasAtras
                                         //Tem bug no site do IG q repete um monte de noticias em uns casos
        {
            string url = _urlBaseIG;
            string html;

            var htmlDoc = new HtmlDocument();

            using (WebClient wc = new WebClient())
            {
                wc.Headers["Cookie"] = "security=true";
                html = wc.DownloadString(url);
            }


            htmlDoc.LoadHtml(html);
            // pegando cada conjunto de noticias de acordo com seu genero
            var noticiaisGenericas = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'nh-transparente')]");
            var noticiasPrincipais = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'nh-noticias')]"); //sao noticias de ultimo-segundo (principais) e de economia, porem nao difere a classe
            var noticiasEntretenimento = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'nh-pop')]");
            var noticiasEsporte = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'nh-esporte')]");

            var listaNoticias = new List<Noticia>();

            #region NOTICIAS GENERICAS (NH-TRANSPARENTE)

            int count = 1;             
            foreach (HtmlNode node in noticiaisGenericas)
            {
                var novaNoticia = new Noticia();
                var novaNoticia2 = new Noticia();
                var novaNoticia3 = new Noticia();
                var novaNoticia4 = new Noticia();

                switch (count)
                {
                    case 1: //Manchete, Duplo1 e Duplo2
                            
                        var todosGenerosPrevia = node.SelectNodes("//span[contains(@class, 'category')]");

                        //Duplo1                         
                        novaNoticia.Titulo = node.SelectSingleNode("//div[2]/div[2]/div/div[1]/div[1]/div[1]/a/img").GetAttributeValue("title", string.Empty);
                        novaNoticia.UrlImage = node.SelectSingleNode("//div[2]/div[2]/div/div[1]/div[1]/div[1]/a/img").GetAttributeValue("data-cfsrc", string.Empty);
                        novaNoticia.Link = node.SelectSingleNode("//div[2]/div[2]/div/div[1]/div[1]/div[1]/a").GetAttributeValue("href", string.Empty);
                        novaNoticia.Fonte = node.SelectSingleNode("//div[2]/div[2]/div/div[1]/div[1]/span/span[1]/meta").GetAttributeValue("content", string.Empty); //busca o span com class "author"                        
                        var tipoNoticia = todosGenerosPrevia[0].GetAttributeValue("class", string.Empty).Remove(0,9); //Pega genero da noticia
                        novaNoticia.TipoNoticia = VerificarTipoNoticia(tipoNoticia);
                        listaNoticias.Add(novaNoticia);

                        novaNoticia2.Titulo = node.SelectSingleNode("//div[2]/div[2]/div/div[2]/div[1]/div[1]/a/img").GetAttributeValue("title", string.Empty);
                        novaNoticia2.UrlImage = node.SelectSingleNode("//div[2]/div[2]/div/div[2]/div[1]/div[1]/a/img").GetAttributeValue("data-cfsrc", string.Empty);
                        novaNoticia2.Link = node.SelectSingleNode("//div[2]/div[2]/div/div[2]/div[1]/div[1]/a").GetAttributeValue("href", string.Empty);
                        novaNoticia2.Fonte = node.SelectSingleNode("//div[2]/div[2]/div/div[2]/div[1]/span/span[1]/meta").GetAttributeValue("content", string.Empty); //busca o span com class "author"
                        tipoNoticia = todosGenerosPrevia[1].GetAttributeValue("class", string.Empty).Remove(0, 9); //Pega genero da noticia
                        novaNoticia2.TipoNoticia = VerificarTipoNoticia(tipoNoticia);
                        listaNoticias.Add(novaNoticia2);

                        //Duplo2
                        novaNoticia3.Titulo = node.SelectSingleNode("//div[2]/div[3]/div/div[1]/div[1]/div[1]/a/img").GetAttributeValue("title", string.Empty);
                        novaNoticia3.UrlImage = node.SelectSingleNode("//div[2]/div[3]/div/div[1]/div[1]/div[1]/a/img").GetAttributeValue("data-cfsrc", string.Empty);
                        novaNoticia3.Link = node.SelectSingleNode("//div[2]/div[3]/div/div[1]/div[1]/div[1]/a").GetAttributeValue("href", string.Empty);
                        novaNoticia3.Fonte = node.SelectSingleNode("//div[2]/div[3]/div/div[1]/div[1]/span/span[1]/meta").GetAttributeValue("content", string.Empty); //busca o span com class "author"
                        tipoNoticia = todosGenerosPrevia[2].GetAttributeValue("class", string.Empty).Remove(0, 9); //Pega genero da noticia
                        novaNoticia3.TipoNoticia = VerificarTipoNoticia(tipoNoticia);
                        listaNoticias.Add(novaNoticia3);

                        novaNoticia4.Titulo = node.SelectSingleNode("//div[2]/div[3]/div/div[2]/div[1]/div[1]/a/img").GetAttributeValue("title", string.Empty);
                        novaNoticia4.UrlImage = node.SelectSingleNode("//div[2]/div[3]/div/div[2]/div[1]/div[1]/a/img").GetAttributeValue("data-cfsrc", string.Empty);
                        novaNoticia4.Link = node.SelectSingleNode("//div[2]/div[3]/div/div[2]/div[1]/div[1]/a").GetAttributeValue("href", string.Empty);
                        novaNoticia4.Fonte = node.SelectSingleNode("//div[2]/div[3]/div/div[2]/div[1]/span/span[1]/meta").GetAttributeValue("content", string.Empty); //busca o span com class "author"
                        tipoNoticia = todosGenerosPrevia[3].GetAttributeValue("class", string.Empty).Remove(0, 9); //Pega genero da noticia
                        novaNoticia4.TipoNoticia = VerificarTipoNoticia(tipoNoticia);
                        listaNoticias.Add(novaNoticia4);

                        count++;
                        break;

                    case 2: //Carrossel, SemFoto1, Duplo e SemFoto2
                            
                        var noticiasCarrossel = node.SelectNodes("//div[contains(@class, 'swiper-slide-item')]");
                        
                        //Carrossel
                        for (var i = 0; i < 5; i++) //Aqui nao consigo buscar o TipoNoticia
                        {
                            var novaNoticia1 = new Noticia();
                            novaNoticia1.Titulo = noticiasCarrossel[i].SelectSingleNode("./a").GetAttributeValue("title", string.Empty);
                            novaNoticia1.UrlImage = noticiasCarrossel[i].SelectSingleNode("./a/img").GetAttributeValue("data-cfsrc", string.Empty);
                            novaNoticia1.Link = noticiasCarrossel[i].SelectSingleNode("./a").GetAttributeValue("href", string.Empty);
                            var fontePrevia = novaNoticia1.Link.Split(".");
                            novaNoticia1.Fonte = fontePrevia[1].ToUpper();
                            listaNoticias.Add(novaNoticia1);
                        }

                        //Duplo
                        novaNoticia.Titulo = node.SelectSingleNode("./div[3]/div/div[1]/div[1]/div[1]/a/img").GetAttributeValue("title", string.Empty);
                        novaNoticia.UrlImage = node.SelectSingleNode("./div[3]/div/div[1]/div[1]/div[1]/a/img").GetAttributeValue("data-cfsrc", string.Empty);
                        novaNoticia.Link = node.SelectSingleNode("./div[3]/div/div[1]/div[1]/div[1]/a").GetAttributeValue("href", string.Empty);
                        novaNoticia.Fonte = node.SelectSingleNode("./div[3]/div/div[1]/div[1]/span/span[1]/meta").GetAttributeValue("content", string.Empty); //busca o span com class "author"                        
                        tipoNoticia = node.SelectSingleNode("./div[3]/div/div[1]").GetAttributeValue("class", string.Empty).Remove(0, 20); //Pega genero da noticia
                        novaNoticia.TipoNoticia = VerificarTipoNoticia(tipoNoticia);
                        listaNoticias.Add(novaNoticia);

                        novaNoticia2.Titulo = node.SelectSingleNode("./div[3]/div/div[2]/div[1]/div[1]/a/img").GetAttributeValue("title", string.Empty);
                        novaNoticia2.UrlImage = node.SelectSingleNode("./div[3]/div/div[2]/div[1]/div[1]/a/img").GetAttributeValue("data-cfsrc", string.Empty);
                        novaNoticia2.Link = node.SelectSingleNode("./div[3]/div/div[2]/div[1]/div[1]/a").GetAttributeValue("href", string.Empty);
                        novaNoticia2.Fonte = node.SelectSingleNode("./div[3]/div/div[2]/div[1]/span/span[1]/meta").GetAttributeValue("content", string.Empty); //busca o span com class "author"                        
                        tipoNoticia = node.SelectSingleNode("./div[3]/div/div[2]").GetAttributeValue("class", string.Empty).Remove(0, 20); //Pega genero da noticia
                        novaNoticia2.TipoNoticia = VerificarTipoNoticia(tipoNoticia);
                        listaNoticias.Add(novaNoticia2);

                        count++;
                        break;

                    case 3: //MaiorDireita e Duplo

                        var todosGenerosPrevia2 = node.SelectNodes("//span[contains(@class, 'category')]");

                        //MaiorDireita                       
                        novaNoticia.Titulo = node.SelectSingleNode("./div[1]/div/div[1]/div[2]/span/meta[2]").GetAttributeValue("content", string.Empty);
                        novaNoticia.UrlImage = node.SelectSingleNode("./div[1]/div/div[1]/div[1]/div/span/meta[2]").GetAttributeValue("content", string.Empty);
                        novaNoticia.Link = node.SelectSingleNode("./div[1]/div/div[1]/div[2]/span/meta[3]").GetAttributeValue("content", string.Empty);
                        novaNoticia.Fonte = node.SelectSingleNode("./div[1]/div/div[1]/div[2]/span/span[1]/meta").GetAttributeValue("content", string.Empty); //busca o span com class "author"                        
                        tipoNoticia = todosGenerosPrevia2[8].GetAttributeValue("class", string.Empty).Remove(0, 9); //Pega genero da noticia
                        novaNoticia.TipoNoticia = VerificarTipoNoticia(tipoNoticia);
                        listaNoticias.Add(novaNoticia);

                        //Duplo
                        novaNoticia2.Titulo = node.SelectSingleNode("./div[2]/div/div[1]/div[1]/div[1]/a/img").GetAttributeValue("title", string.Empty);
                        novaNoticia2.UrlImage = node.SelectSingleNode("./div[2]/div/div[1]/div[1]/div[1]/a/img").GetAttributeValue("data-cfsrc", string.Empty);
                        novaNoticia2.Link = node.SelectSingleNode("./div[2]/div/div[1]/div[1]/div[1]/a").GetAttributeValue("href", string.Empty);
                        novaNoticia2.Fonte = node.SelectSingleNode("./div[2]/div/div[1]/div[1]/span/span[1]/meta").GetAttributeValue("content", string.Empty); //busca o span com class "author"                        
                        tipoNoticia = todosGenerosPrevia2[9].GetAttributeValue("class", string.Empty).Remove(0, 9); //Pega genero da noticia
                        novaNoticia2.TipoNoticia = VerificarTipoNoticia(tipoNoticia);
                        listaNoticias.Add(novaNoticia2);

                        novaNoticia3.Titulo = node.SelectSingleNode("./div[2]/div/div[2]/div[1]/div[1]/a/img").GetAttributeValue("title", string.Empty);
                        novaNoticia3.UrlImage = node.SelectSingleNode("./div[2]/div/div[2]/div[1]/div[1]/a/img").GetAttributeValue("data-cfsrc", string.Empty);
                        novaNoticia3.Link = node.SelectSingleNode("./div[2]/div/div[2]/div[1]/div[1]/a").GetAttributeValue("href", string.Empty);
                        novaNoticia3.Fonte = node.SelectSingleNode("./div[2]/div/div[2]/div[1]/span/span[1]/meta").GetAttributeValue("content", string.Empty); //busca o span com class "author"                        
                        tipoNoticia = todosGenerosPrevia2[10].GetAttributeValue("class", string.Empty).Remove(0, 9); //Pega genero da noticia
                        novaNoticia3.TipoNoticia = VerificarTipoNoticia(tipoNoticia);
                        listaNoticias.Add(novaNoticia3);

                        count++;
                        break;

                    case 4: case 5: case 7: //Individuais

                        var todosGenerosPrevia3 = node.SelectNodes("//span[contains(@class, 'category')]");
                        
                        novaNoticia.Titulo = node.SelectSingleNode("./div/div/div[1]/div[1]/div/a/img").GetAttributeValue("title", string.Empty);
                        novaNoticia.UrlImage = node.SelectSingleNode("./div/div/div[1]/div[1]/div/a/img").GetAttributeValue("data-cfsrc", string.Empty);
                        novaNoticia.Link = node.SelectSingleNode("./div/div/div[1]/div[1]/div/a").GetAttributeValue("href", string.Empty);
                        novaNoticia.Fonte = node.SelectSingleNode("./div/div/div[1]/div[2]/span/span[1]/meta").GetAttributeValue("content", string.Empty); //busca o span com class "author"                        
                        tipoNoticia = todosGenerosPrevia3[count + 7].GetAttributeValue("class", string.Empty).Remove(0, 9); //Pega genero da noticia
                        novaNoticia.TipoNoticia = VerificarTipoNoticia(tipoNoticia);
                        listaNoticias.Add(novaNoticia);

                        count++;
                        break;

                    case 6: //SemFoto e Duplo

                        var todosGenerosPrevia4 = node.SelectNodes("//span[contains(@class, 'category')]");

                        //Duplo
                        novaNoticia.Titulo = node.SelectSingleNode("./div[2]/div/div[1]/div[1]/div[1]/a/img").GetAttributeValue("title", string.Empty);
                        novaNoticia.UrlImage = node.SelectSingleNode("./div[2]/div/div[1]/div[1]/div[1]/a/img").GetAttributeValue("data-cfsrc", string.Empty);
                        novaNoticia.Link = node.SelectSingleNode("./div[2]/div/div[1]/div[1]/div[1]/a").GetAttributeValue("href", string.Empty);
                        novaNoticia.Fonte = node.SelectSingleNode("./div[2]/div/div[1]/div[1]/span/span[1]/meta").GetAttributeValue("content", string.Empty); //busca o span com class "author"                        
                        tipoNoticia = todosGenerosPrevia4[14].GetAttributeValue("class", string.Empty).Remove(0, 9); //Pega genero da noticia
                        novaNoticia.TipoNoticia = VerificarTipoNoticia(tipoNoticia);
                        listaNoticias.Add(novaNoticia);

                        novaNoticia2.Titulo = node.SelectSingleNode("./div[2]/div/div[2]/div[1]/div[1]/a/img").GetAttributeValue("title", string.Empty); //VER PQ TA BUG SE TIVER ASPAS
                        novaNoticia2.UrlImage = node.SelectSingleNode("./div[2]/div/div[2]/div[1]/div[1]/a/img").GetAttributeValue("data-cfsrc", string.Empty);
                        novaNoticia2.Link = node.SelectSingleNode("./div[2]/div/div[2]/div[1]/div[1]/a").GetAttributeValue("href", string.Empty);
                        novaNoticia2.Fonte = node.SelectSingleNode("./div[2]/div/div[2]/div[1]/span/span[1]/meta").GetAttributeValue("content", string.Empty); //busca o span com class "author"                        
                        tipoNoticia = todosGenerosPrevia4[15].GetAttributeValue("class", string.Empty).Remove(0, 9); //Pega genero da noticia
                        novaNoticia2.TipoNoticia = VerificarTipoNoticia(tipoNoticia);
                        listaNoticias.Add(novaNoticia2);

                        count++;
                        break;
                }                               
            }

            #endregion

            #region NOTICIAS PRINCIPAIS (NH-NOTICIAS)            

            count = 1;
            foreach (HtmlNode node in noticiasPrincipais)
            {
                var novaNoticia = new Noticia();
                var novaNoticia2 = new Noticia();

                switch (count)
                {
                    case 2: //CardFoto e SemFoto

                        //CardFoto
                        novaNoticia.Titulo = node.SelectSingleNode("./div[1]/div/div[2]/h2/a").GetAttributeValue("title", string.Empty);
                        novaNoticia.UrlImage = node.SelectSingleNode("./div[1]/div/div[1]/span/meta[2]").GetAttributeValue("content", string.Empty);
                        novaNoticia.Link = node.SelectSingleNode("./div[1]/div/div[2]/h2/a").GetAttributeValue("href", string.Empty);
                        novaNoticia.Fonte = node.SelectSingleNode("./div[1]/div/div[2]/span/span[1]/meta").GetAttributeValue("content", string.Empty);
                        novaNoticia.TipoNoticia = TipoNoticia.Principal;
                        listaNoticias.Add(novaNoticia);

                        count++;
                        break;

                    case 3: //SemFoto1, Foto, SemFoto2

                        //Foto
                        novaNoticia.Titulo = node.SelectSingleNode("./div[2]/div/div[1]/div[2]/h2/a").GetAttributeValue("title", string.Empty);
                        novaNoticia.UrlImage = node.SelectSingleNode("./div[2]/div/div[1]/div[1]/div/a/img").GetAttributeValue("data-cfsrc", string.Empty);
                        novaNoticia.Link = node.SelectSingleNode("./div[2]/div/div[1]/div[2]/h2/a").GetAttributeValue("href", string.Empty);
                        novaNoticia.Fonte = node.SelectSingleNode("./div[2]/div/div[1]/div[2]/span/span[1]/meta").GetAttributeValue("content", string.Empty);
                        novaNoticia.TipoNoticia = TipoNoticia.Principal;
                        listaNoticias.Add(novaNoticia);

                        count++;
                        break;

                    case 4: //Video e Foto

                        //Foto
                        novaNoticia.Titulo = node.SelectSingleNode("./div[2]/div/div[1]/div[2]/h2/a").GetAttributeValue("title", string.Empty);
                        novaNoticia.UrlImage = node.SelectSingleNode("./div[2]/div/div[1]/div[1]/div/a/img").GetAttributeValue("data-cfsrc", string.Empty);
                        novaNoticia.Link = node.SelectSingleNode("./div[2]/div/div[1]/div[2]/h2/a").GetAttributeValue("href", string.Empty);
                        novaNoticia.Fonte = node.SelectSingleNode("./div[2]/div/div[1]/div[2]/span/span[1]/meta").GetAttributeValue("content", string.Empty);
                        novaNoticia.TipoNoticia = TipoNoticia.Principal;
                        listaNoticias.Add(novaNoticia);

                        count++;
                        break;

                    case 5: //Duplo

                        //Duplo
                        novaNoticia.Titulo = node.SelectSingleNode("./div[1]/div/div[1]/div[1]/div[2]/h2/a").GetAttributeValue("title", string.Empty);
                        novaNoticia.UrlImage = node.SelectSingleNode("./div[1]/div/div[1]/div[1]/div[1]/span/meta[2]").GetAttributeValue("content", string.Empty);
                        novaNoticia.Link = node.SelectSingleNode("./div[1]/div/div[1]/div[1]/div[2]/h2/a").GetAttributeValue("href", string.Empty);
                        novaNoticia.Fonte = node.SelectSingleNode("./div[1]/div/div[1]/div[1]/span/span[1]/meta").GetAttributeValue("content", string.Empty);
                        novaNoticia.TipoNoticia = TipoNoticia.Principal;
                        listaNoticias.Add(novaNoticia);

                        novaNoticia2.Titulo = node.SelectSingleNode("./div[1]/div/div[2]/div[1]/div[2]/h2/a").GetAttributeValue("title", string.Empty);
                        novaNoticia2.UrlImage = node.SelectSingleNode("./div[1]/div/div[2]/div[1]/div[1]/span/meta[2]").GetAttributeValue("content", string.Empty);
                        novaNoticia2.Link = node.SelectSingleNode("./div[1]/div/div[2]/div[1]/div[2]/h2/a").GetAttributeValue("href", string.Empty);
                        novaNoticia2.Fonte = node.SelectSingleNode("./div[1]/div/div[2]/div[1]/span/span[1]/meta").GetAttributeValue("content", string.Empty);
                        novaNoticia2.TipoNoticia = TipoNoticia.Principal;
                        listaNoticias.Add(novaNoticia2);

                        count++;
                        break;

                    default:
                        count++;
                        break;
                }
            }

            #endregion

            #region NOTICIAS ENTRETENIMENTO (NH-POP)            

            count = 1;
            foreach (HtmlNode node in noticiasEntretenimento)
            {
                var novaNoticia = new Noticia();
                var novaNoticia2 = new Noticia();
                var novaNoticia3 = new Noticia();

                switch (count)
                {
                    case 2: //DestaqueFoto e Duplo

                        //DestaqueFoto
                        novaNoticia.Titulo = node.SelectSingleNode("./div[1]/div/div[2]/h2/a").GetAttributeValue("title", string.Empty);
                        novaNoticia.UrlImage = node.SelectSingleNode("./div[1]/div/div[1]/span/meta[2]").GetAttributeValue("content", string.Empty);
                        novaNoticia.Link = node.SelectSingleNode("./div[1]/div/div[2]/h2/a").GetAttributeValue("href", string.Empty);
                        novaNoticia.Fonte = node.SelectSingleNode("./div[1]/div/div[2]/span/span[1]/meta").GetAttributeValue("content", string.Empty);
                        novaNoticia.TipoNoticia = TipoNoticia.Entretenimento;
                        listaNoticias.Add(novaNoticia);

                        //Duplo
                        novaNoticia3.Titulo = node.SelectSingleNode("./div[2]/div/div[1]/div[1]/div[2]/h2/a").GetAttributeValue("title", string.Empty);
                        novaNoticia3.UrlImage = node.SelectSingleNode("./div[2]/div/div[1]/div[1]/div[1]/span/meta[2]").GetAttributeValue("content", string.Empty);
                        novaNoticia3.Link = node.SelectSingleNode("./div[2]/div/div[1]/div[1]/div[2]/h2/a").GetAttributeValue("href", string.Empty);
                        novaNoticia3.Fonte = node.SelectSingleNode("./div[2]/div/div[1]/div[1]/span/span[1]/meta").GetAttributeValue("content", string.Empty);
                        novaNoticia3.TipoNoticia = TipoNoticia.Entretenimento;
                        listaNoticias.Add(novaNoticia3);

                        novaNoticia2.Titulo = node.SelectSingleNode("./div[2]/div/div[2]/div[1]/div[2]/h2/a").GetAttributeValue("title", string.Empty);
                        novaNoticia2.UrlImage = node.SelectSingleNode("./div[2]/div/div[2]/div[1]/div[1]/span/meta[2]").GetAttributeValue("content", string.Empty);
                        novaNoticia2.Link = node.SelectSingleNode("./div[2]/div/div[2]/div[1]/div[2]/h2/a").GetAttributeValue("href", string.Empty);
                        novaNoticia2.Fonte = node.SelectSingleNode("./div[2]/div/div[2]/div[1]/span/span[1]/meta").GetAttributeValue("content", string.Empty);
                        novaNoticia2.TipoNoticia = TipoNoticia.Entretenimento;
                        listaNoticias.Add(novaNoticia2);

                        count++;
                        break;

                    case 3: case 4://DestaqueFoto, SemFoto e Foto

                        //DestaqueFoto
                        novaNoticia.Titulo = node.SelectSingleNode("./div[1]/div/div[1]/div[2]/h2/a").GetAttributeValue("title", string.Empty);
                        novaNoticia.UrlImage = node.SelectSingleNode("./div[1]/div/div[1]/div[1]/div/span/meta[2]").GetAttributeValue("content", string.Empty);
                        novaNoticia.Link = node.SelectSingleNode("./div[1]/div/div[1]/div[2]/h2/a").GetAttributeValue("href", string.Empty);
                        novaNoticia.Fonte = node.SelectSingleNode("./div[1]/div/div[1]/div[2]/span/span[1]/meta").GetAttributeValue("content", string.Empty);
                        novaNoticia.TipoNoticia = TipoNoticia.Entretenimento;
                        listaNoticias.Add(novaNoticia);

                        //Foto
                        novaNoticia2.Titulo = node.SelectSingleNode("./div[3]/div/div[1]/div[2]/h2/a").GetAttributeValue("title", string.Empty);
                        novaNoticia2.UrlImage = node.SelectSingleNode("./div[3]/div/div[1]/div[1]/div/span/meta[2]").GetAttributeValue("content", string.Empty);
                        novaNoticia2.Link = node.SelectSingleNode("./div[3]/div/div[1]/div[2]/h2/a").GetAttributeValue("href", string.Empty);
                        novaNoticia2.Fonte = node.SelectSingleNode("./div[3]/div/div[1]/div[2]/span/span[1]/meta").GetAttributeValue("content", string.Empty);
                        novaNoticia2.TipoNoticia = TipoNoticia.Entretenimento;
                        listaNoticias.Add(novaNoticia2);

                        count++;
                        break;                    

                    default:
                        count++;
                        break;
                }
            }

            #endregion

            #region NOTICIAS ESPORTES (NH-ESPORTE)            

            count = 1;
            foreach (HtmlNode node in noticiasEsporte)
            {
                var novaNoticia = new Noticia();
                var novaNoticia2 = new Noticia();                

                switch (count)
                {
                    case 2: //Manchete

                        //Manchete
                        novaNoticia.Titulo = node.SelectSingleNode("./div/div/div[2]/h2/a").GetAttributeValue("title", string.Empty);
                        novaNoticia.UrlImage = node.SelectSingleNode("./div/div/div[1]/span/meta[2]").GetAttributeValue("content", string.Empty);
                        novaNoticia.Link = node.SelectSingleNode("./div/div/div[2]/h2/a").GetAttributeValue("href", string.Empty);
                        novaNoticia.Fonte = node.SelectSingleNode("./div/div/div[2]/span/span[1]/meta").GetAttributeValue("content", string.Empty);
                        novaNoticia.TipoNoticia = TipoNoticia.Esportes;
                        listaNoticias.Add(novaNoticia);

                        count++;
                        break;

                    case 3: case 5: case 6: case 8: //Individuais

                        //Individuais
                        novaNoticia.Titulo = node.SelectSingleNode("./div/div/div[1]/div[2]/h2/a").GetAttributeValue("title", string.Empty);
                        novaNoticia.UrlImage = node.SelectSingleNode("./div/div/div[1]/div[1]/div/span/meta[2]").GetAttributeValue("content", string.Empty);
                        novaNoticia.Link = node.SelectSingleNode("./div/div/div[1]/div[2]/h2/a").GetAttributeValue("href", string.Empty);
                        novaNoticia.Fonte = node.SelectSingleNode("./div/div/div[1]/div[2]/span/span[1]/meta").GetAttributeValue("content", string.Empty);
                        novaNoticia.TipoNoticia = TipoNoticia.Esportes;
                        listaNoticias.Add(novaNoticia);

                        count++;
                        break;

                    case 7: //SemFoto e Duplo

                        //Duplo
                        novaNoticia.Titulo = node.SelectSingleNode("./div[2]/div/div[1]/div[1]/div[2]/h2/a").GetAttributeValue("title", string.Empty);
                        novaNoticia.UrlImage = node.SelectSingleNode("./div[2]/div/div[1]/div[1]/div[1]/span/meta[2]").GetAttributeValue("content", string.Empty);
                        novaNoticia.Link = node.SelectSingleNode("./div[2]/div/div[1]/div[1]/div[2]/h2/a").GetAttributeValue("href", string.Empty);
                        novaNoticia.Fonte = node.SelectSingleNode("./div[2]/div/div[1]/div[1]/span/span[1]/meta").GetAttributeValue("content", string.Empty);
                        novaNoticia.TipoNoticia = TipoNoticia.Esportes;
                        listaNoticias.Add(novaNoticia);

                        novaNoticia2.Titulo = node.SelectSingleNode("./div[2]/div/div[2]/div[1]/div[2]/h2/a").GetAttributeValue("title", string.Empty);
                        novaNoticia2.UrlImage = node.SelectSingleNode("./div[2]/div/div[2]/div[1]/div[1]/span/meta[2]").GetAttributeValue("content", string.Empty);
                        novaNoticia2.Link = node.SelectSingleNode("./div[2]/div/div[2]/div[1]/div[2]/h2/a").GetAttributeValue("href", string.Empty);
                        novaNoticia2.Fonte = node.SelectSingleNode("./div[2]/div/div[2]/div[1]/span/span[1]/meta").GetAttributeValue("content", string.Empty);
                        novaNoticia2.TipoNoticia = TipoNoticia.Esportes;
                        listaNoticias.Add(novaNoticia2);

                        count++;
                        break;

                    default:
                        count++;
                        break;
                }
            }

            #endregion

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
                        noticia.OrigemNoticia = OrigemNoticia.IG;
                        noticia.CriadoEm = DateTime.Now;
                        _noticiaRepository.AdicionarNoticia(noticia);
                    }
                }               
            }
        }

        private TipoNoticia VerificarTipoNoticia (string tipoNoticia)
        {
            TipoNoticia response;
            switch (tipoNoticia)
            {
                case "ultimo-segundo": 
                    response = TipoNoticia.Principal;
                    break;

                case "economia":
                    response = TipoNoticia.Negocios;
                    break;

                case "pop":
                    response = TipoNoticia.Entretenimento;
                    break;

                case "esporte":
                    response = TipoNoticia.Esportes;
                    break;

                default:
                    response = TipoNoticia.Outro;
                    break;
            }
            return response;
        }
    }
}
