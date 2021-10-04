using Domain.Models;
using Domain.Util;
using System.Collections.Generic;

namespace Domain.Validations
{
    public class NoticiaValidation
    {
        public NoticiaValidation()
        {
        }

        public void VerificarExistenciaNoticia(Noticia noticia)
        {
            var errosResponse = new List<string>(0);

            if (noticia == null)
            {
                errosResponse.Add("A Notícia informada não existe.");
                throw new ParametroException(errosResponse);
            }
        }

        //public void ValidarSalvarUsuario(Usuario usuario)
        //{
        //    var errosResponse = new List<string>(0);

        //    if (usuario.Nome == null || usuario.Nome == "")
        //    {
        //        errosResponse.Add("Campo 'Nome' é obrigatório.");
        //    }
        //    if (usuario.Email == null || usuario.Email == "")
        //    {
        //        errosResponse.Add("Campo 'Email' é obrigatório.");
        //    }            
        //    if (usuario.Senha == null || usuario.Senha == "")
        //    {
        //        errosResponse.Add("Campo 'Senha' é obrigatório.");
        //    }           

        //    if (errosResponse.Count > 0)
        //    {
        //        throw new ParametroException(errosResponse);
        //    }
        //}

        public Noticia CompararNoticia(Noticia edit, Noticia noticia)
        {
            if (!string.IsNullOrEmpty(edit.Titulo))
            {
                noticia.Titulo = edit.Titulo;
            }
            if (!string.IsNullOrEmpty(edit.UrlImage))
            {
                noticia.UrlImage = edit.UrlImage;
            }
            if (!string.IsNullOrEmpty(edit.Fonte))
            {
                noticia.Fonte = edit.Fonte;
            }
            if (!string.IsNullOrEmpty(edit.Link))
            {
                noticia.Link = edit.Link;
            }
            if (!string.IsNullOrEmpty(edit.HoraAtras))
            {
                noticia.HoraAtras = edit.HoraAtras;
            }
            if(edit.TipoNoticia != noticia.TipoNoticia)
            {
                noticia.TipoNoticia = edit.TipoNoticia;
            }
            noticia.AtualizadoEm = System.DateTime.Now;
            //noticia.IdAtualizadoPor = Id do usuario

            return noticia;
        }

    }
}
