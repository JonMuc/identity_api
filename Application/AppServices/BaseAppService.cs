using Domain.Interfaces.Repository;

namespace Application.AppServices
{
    public class BaseAppService
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseAppService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
