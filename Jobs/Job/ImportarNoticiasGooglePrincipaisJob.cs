using Domain.Interfaces;
using Domain.Services;

namespace Jobs.Job
{
    public class ImportarNoticiasGooglePrincipaisJob : BaseJob
    {
        protected readonly IUsuarioRepository _usuarioRepository;
        protected readonly IUsuarioService _usuarioService;

        public ImportarNoticiasGooglePrincipaisJob(IUsuarioRepository usuarioRepository, IUsuarioService usuarioService)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioService = usuarioService;
        }


        public override void Process()
        {
            //METODOS
        }

        protected override void Init()
        {
            var teste = _usuarioRepository.BuscarUsuarioPorId(1);
        }
    }
}
