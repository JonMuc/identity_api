using Domain.Models;
using Domain.Util;
using System.Collections.Generic;

namespace Domain.Validations
{
    public class NoticiaFavoritoValidation
    {
        public NoticiaFavoritoValidation()
        {
        }

        public void VerificarExistenciaNoticiaFavorito(NoticiaFavorito noticiaFavorito)
        {
            var errosResponse = new List<string>(0);

            if (noticiaFavorito == null)
            {
                errosResponse.Add("A Notícia informada não existe.");
                throw new ParametroException(errosResponse);
            }           
        }        

        public List<Noticia> ValidarListaFavoritos(List<Noticia> list)
        {
            var errosResponse = new List<string>(0);
            var listaValidada = new List<Noticia>();

            if (list == null || list.Count == 0)
            {
                errosResponse.Add("Não existe notícias favoritadas vinculadas a este usuário.");
                throw new ParametroException(errosResponse);
            }

            foreach (Noticia noticia in list)
            {
                if (noticia.StatusRegistro == 0)
                {
                    listaValidada.Add(noticia);
                }
            }
            
            if (listaValidada == null)
            {
                errosResponse.Add("Não existe notícias favoritadas ativas para essa consulta.");
                throw new ParametroException(errosResponse);
            }
            return listaValidada;
        }

        public NoticiaFavorito CompararNoticiaFavorito(NoticiaFavorito edit, NoticiaFavorito noticiaFavorito)
        {
            if (edit.StatusRegistro != noticiaFavorito.StatusRegistro)
            {
                noticiaFavorito.StatusRegistro = edit.StatusRegistro;
            }
            if (edit.IdNoticia != noticiaFavorito.IdNoticia)
            {
                noticiaFavorito.IdNoticia = edit.IdNoticia;
            }
            if (edit.IdUsuario != noticiaFavorito.IdUsuario) //acho q nao deveria mudar isso, só a noticia vinculada
            {
                noticiaFavorito.IdUsuario = edit.IdUsuario;
            }

            noticiaFavorito.AtualizadoEm = System.DateTime.Now;
            //noticiaFavorito.IdAtualizadoPor = Id do usuario

            return noticiaFavorito;
        }

    }
}
