using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Exceptions;
using EventPlanner.Domain.Repositories;
using MediatR;

namespace EventPlanner.Business.UseCases.ViewLocation
{
    public sealed class ViewLocationHandler : IRequestHandler<ViewLocationRequest, ViewLocationResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ViewLocationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ViewLocationResponse> Handle(ViewLocationRequest request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Locations.GetAllAsync();

            if (result == null || !result.Any())
            {
                throw new NotFoundException(typeof(Location));
            }

            return new ViewLocationResponse() { Locations = result };
        }
    }
}
