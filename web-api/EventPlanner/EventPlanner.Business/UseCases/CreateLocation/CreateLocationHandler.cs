using AutoMapper;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Repositories;
using MediatR;

namespace EventPlanner.Business.UseCases.CreateLocation
{
    public sealed class CreateLocationHandler : IRequestHandler<CreateLocationRequest, Location>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLocationHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<Location> Handle(CreateLocationRequest request, CancellationToken cancellationToken)
        {
            Location location = _mapper.Map<Location>(request);
            Location result = await _unitOfWork.Locations.CreateAsync(location);

            await _unitOfWork.CommitAsync(cancellationToken);

            return result;
        }
    }
}
