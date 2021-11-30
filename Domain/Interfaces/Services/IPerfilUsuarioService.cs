using Domain.Models;
using Domain.Models.Enums;
using Domain.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IPerfilUsuarioService
    {
        public Task<long> AdicionarPerfilUsuarioAsync(PerfilUsuarioRequest request);        
        public Task DeletarPerfilUsuarioByIdAsync(long idPerfil);
        public Task<IEnumerable<TipoNoticia>> VisualizarPerfilUsuarioCompleto(long idUsuario);
    }
}
