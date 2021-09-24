using Domain.Interfaces;
using Domain.Services;

namespace Jobs.Job
{
    public class ImportarNoticiasG1PrincipaisJob : BaseJob
    {
        protected readonly IUsuarioRepository _usuarioRepository;
        protected readonly IUsuarioService _usuarioService;

        public ImportarNoticiasG1PrincipaisJob(IUsuarioRepository usuarioRepository, IUsuarioService usuarioService)
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
