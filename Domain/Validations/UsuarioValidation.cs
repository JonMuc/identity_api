﻿using Domain.Models;
using Domain.Util;
using System.Collections.Generic;

namespace Domain.Validations
{
    public class UsuarioValidation
    {
        public UsuarioValidation()
        {
        }

        public void VerificarExistenciaUsuario(Usuario usuario)
        {
            var errosResponse = new List<string>(0);

            if (usuario == null)
            {
                errosResponse.Add("Usuário informado não existe.");
                throw new ParametroException(errosResponse);
            }
        }

        public void ValidarSalvarUsuario(Usuario usuario)
        {
            var errosResponse = new List<string>(0);

            if (usuario.Nome == null || usuario.Nome == "")
            {
                errosResponse.Add("Campo 'Nome' é obrigatório.");
            }
            if (usuario.Email == null || usuario.Email == "")
            {
                errosResponse.Add("Campo 'Email' é obrigatório.");
            }            
            if (usuario.Senha == null || usuario.Senha == "")
            {
                errosResponse.Add("Campo 'Senha' é obrigatório.");
            }           

            if (errosResponse.Count > 0)
            {
                throw new ParametroException(errosResponse);
            }
        }

        public Usuario CompararUsuario(Usuario edit, Usuario user)
        {
            if (!string.IsNullOrEmpty(edit.Nome))
            {
                user.Nome = edit.Nome;
            }
            if (!string.IsNullOrEmpty(edit.Email))
            {
                user.Email = edit.Email;
            }
            if (!string.IsNullOrEmpty(edit.Senha))
            {
                user.Senha = edit.Senha;
            }
            if (!string.IsNullOrEmpty(edit.Telefone))
            {
                user.Telefone = edit.Telefone;
            }
            if (!string.IsNullOrEmpty(edit.Foto))
            {
                user.Foto = edit.Foto;
            }
            if (!string.IsNullOrEmpty(edit.IdGoogle))
            {
                user.IdGoogle = edit.IdGoogle;
            }
            if (!string.IsNullOrEmpty(edit.IdFacebook))
            {
                user.IdFacebook = edit.IdFacebook;
            }
            if (!string.IsNullOrEmpty(edit.PerfilLinkedin))
            {
                user.PerfilLinkedin = edit.PerfilLinkedin;
            }
            if (!string.IsNullOrEmpty(edit.PerfilInstagram))
            {
                user.PerfilInstagram = edit.PerfilInstagram;
            }
            if (!string.IsNullOrEmpty(edit.PerfilTwitter))
            {
                user.PerfilTwitter = edit.PerfilTwitter;
            }
            if (!string.IsNullOrEmpty(edit.Descricao))
            {
                user.Descricao = edit.Descricao;
            }

            return user;
        }

    }
}
