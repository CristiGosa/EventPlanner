using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Exceptions;
using EventPlanner.Domain.Repositories;
using MediatR;

namespace EventPlanner.Business.UseCases.ViewLocationById
{
    public sealed class ViewLocationByIdHandler : IRequestHandler<ViewLocationByIdRequest, ViewLocationByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ViewLocationByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ViewLocationByIdResponse> Handle(ViewLocationByIdRequest request, CancellationToken cancellationToken)
        {
            var result = _unitOfWork.Locations.GetAllAsync().Result.First(x => x.Id == request.Id);

            if (result == null)
            {
                throw new NotFoundException(typeof(Location));
            }

            return new ViewLocationByIdResponse() { Location = result };
        }
    }
}
