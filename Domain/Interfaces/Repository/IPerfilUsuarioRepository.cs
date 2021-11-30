using Domain.Models;
using Domain.Models.Enums;
using Domain.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPerfilUsuarioRepository
    {
        public Task<long> AdicionarPerfilUsuarioAsync(PerfilUsuario perfilUsuario);
        public Task<PerfilUsuario> GetPerfilUsuarioById(long idPerfil);
        public Task<IEnumerable<TipoNoticia>> VisualizarPerfilUsuarioCompletoAsync(long idUsuario);
        public Task DeletarPerfilUsuarioAsync(long idPerfil);
        public Task<long> VerificarPerfilUsuarioAsync(PerfilUsuarioRequest request);

    }
}
