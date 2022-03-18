using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Dto;
using Domain.Util;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Validations
{
    public class UsuarioValidation
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private Regex validarNomeUSer = new Regex(@"^[a-zA-Z0-9]+$");

        public UsuarioValidation(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
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
            if (!string.IsNullOrEmpty(edit.NomeUsuario))
            {
                user.NomeUsuario = edit.NomeUsuario;
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
            //if (!string.IsNullOrEmpty(edit.PerfilLinkedin))
            //{
            //    user.PerfilLinkedin = edit.PerfilLinkedin;
            //} 
            //if (!string.IsNullOrEmpty(edit.PerfilFacebook))
            //{
            //    user.PerfilFacebook = edit.PerfilFacebook;
            //}
            //if (!string.IsNullOrEmpty(edit.PerfilInstagram))
            //{
            //    user.PerfilInstagram = edit.PerfilInstagram;
            //}
            //if (!string.IsNullOrEmpty(edit.PerfilTwitter))
            //{
            //    user.PerfilTwitter = edit.PerfilTwitter;
            //}
            //if (!string.IsNullOrEmpty(edit.Descricao))
            //{
            //    user.Descricao = edit.Descricao;
            //}
            user.PerfilTwitter = edit.PerfilTwitter;
            user.Descricao = edit.Descricao;
            user.PerfilInstagram = edit.PerfilInstagram;
            user.PerfilFacebook = edit.PerfilFacebook;
            user.PerfilLinkedin = edit.PerfilLinkedin;

            return user;
        }

        public async Task ValidarCriarUsuario(CriarContaUsuario usuario)
        {
            var errosResponse = new List<string>(0);

            if (usuario.Nome == null || usuario.Nome == "")
            {
                errosResponse.Add("Campo 'Nome' é obrigatório.");
            }
            if (usuario.Senha != usuario.ConfirmarSenha)
            {
                errosResponse.Add("As senhas não conferem.");
            }
            if (usuario.Email == null || usuario.Email == "")
            {
                errosResponse.Add("Campo 'Email' é obrigatório.");
            }
            if (usuario.Senha == null || usuario.Senha == "")
            {
                errosResponse.Add("Campo 'Senha' é obrigatório.");
            }
            if (usuario.Email != null && usuario.Email != "" && await _usuarioRepository.VerificarExistenciaEmail(usuario.Email))
            {
                errosResponse.Add("Este e-mail já esta cadastrado.");
            }
            if (await _usuarioRepository.VerificarExistenciaNomeUser(usuario.NomeUsuario))
            {
                errosResponse.Add("Este nome de usuario já está cadastrado.");
            }
            if (!validarNomeUSer.IsMatch(usuario.NomeUsuario))
            {
                errosResponse.Add("Nome de usuario invalido.");
            }

            if (errosResponse.Count > 0)
            {
                throw new ParametroException(errosResponse);
            }
        }
    }
}
