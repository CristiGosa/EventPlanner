using AutoMapper;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Exceptions;
using EventPlanner.Domain.Repositories;
using MediatR;

namespace EventPlanner.Business.UseCases.CreateEvent
{
    public sealed class CreateEventHandler : IRequestHandler<CreateEventRequest, Event>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEventHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Event> Handle(CreateEventRequest request, CancellationToken cancellationToken)
        {
            Event createdEvent = _mapper.Map<Event>(request);

            User organizer = await _unitOfWork.Users.GetByEmailAsync(request.OrganizerEmail);

            if (organizer == null)
            {
                throw new InvalidUserException(request.OrganizerEmail);
            }

            Event result = await _unitOfWork.Events.CreateAsync(createdEvent, organizer);

            await _unitOfWork.CommitAsync(cancellationToken);

            return result;
        }
    }
}

