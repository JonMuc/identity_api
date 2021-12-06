using Domain.Interfaces;
using Domain.Interfaces.Repository;
using System.Threading.Tasks;

namespace Application.AppServices
{
    public class MetricaAppService : BaseAppService
    {       
        private readonly IMetricaRepository _metricaRepository;

        public MetricaAppService(IUnitOfWork unitOfWork, IMetricaRepository metricaRepository) : base(unitOfWork)
        {
            _metricaRepository = metricaRepository;
        }
      
        public async Task ContabilizarClick(long idUsuario, long idNoticia)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                await _metricaRepository.ContabilizarClickAsync(idUsuario, idNoticia);
                _unitOfWork.CommitTransaction();                
            }
        }
    }
}
