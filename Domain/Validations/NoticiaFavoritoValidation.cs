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

        public NoticiaFavorito CompararNoticiaFavorito(NoticiaFavorito edit, NoticiaFavorito noticiaFavorito)
        {
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
