using Domain.Models;
using Domain.Util;
using System.Collections.Generic;

namespace Domain.Validations
{
    public class UsuarioValidation
    {
        public UsuarioValidation()
        {
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
       
    }
}
