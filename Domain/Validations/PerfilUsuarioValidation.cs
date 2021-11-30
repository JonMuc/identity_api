using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Enums;
using Domain.Models.Request;
using Domain.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Validations
{
    public class PerfilUsuarioValidation
    {
        private readonly IPerfilUsuarioRepository _perfilUsuarioRepository;
        
        public PerfilUsuarioValidation(IPerfilUsuarioRepository perfilUsuarioRepository)
        {
            _perfilUsuarioRepository = perfilUsuarioRepository;
        }

        public void VerificarExistenciaPerfil(PerfilUsuario perfil)
        {
            var errosResponse = new List<string>(0);

            if (perfil == null)
            {
                errosResponse.Add("O perfil informado não existe.");
                throw new ParametroException(errosResponse);
            }
        }       

        public void ValidarInclusaoPerfilUsuario(long check, TipoNoticia tipoNoticia)
        {
            var errosResponse = new List<string>(0);
            if (check != 0)
            {
                errosResponse.Add("Este usuário já dispõe desse interesse.");
                throw new ParametroException(errosResponse);
            }

            if (!Enum.IsDefined(typeof(TipoNoticia), tipoNoticia))
            {
                errosResponse.Add("O valor de TipoAvaliação é inválido.");
                throw new ParametroException(errosResponse);
            }
        }

        public IEnumerable<TipoNoticia> ValidarPerfil(IEnumerable<TipoNoticia> list)
        {
            var errosResponse = new List<string>(0);            
            if (list.Count() == 0)
            {
                errosResponse.Add("Não existe lista de interesses para o usuário desejado.");
                throw new ParametroException(errosResponse);
            }
            return list;
        }
    }
}
