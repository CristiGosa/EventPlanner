using EventPlanner.Domain.Entities.Email;
using EventPlanner.Domain.Repositories;
using EventPlanner.Domain.Services;
using MediatR;

namespace EventPlanner.Business.UseCases.UpdateEventStatus;
public sealed class UpdateEventStatusHandler : IRequestHandler<UpdateEventStatusRequest, UpdateEventStatusResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private IEmailService _emailService;

    public UpdateEventStatusHandler(IUnitOfWork unitOfWork, IEmailService emailService)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
    }

    public async Task<UpdateEventStatusResponse> Handle(UpdateEventStatusRequest request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Events.UpdateStatusAsync(request.EventId, request.NewStatus);

        var requestedEvent = _unitOfWork.Events.GetAllAsync(x => x.Id == request.EventId).Result.FirstOrDefault();
        var requestedLocation = _unitOfWork.Locations.GetAllAsync().Result.FirstOrDefault(x => x.Id == requestedEvent.LocationId);
        var organizer = await _unitOfWork.Users.GetByEmailAsync(requestedEvent.OrganizerEmail);

        var receiverInfo = new ReceiverInfo
        {
            FirstName = organizer.Name,
            Email = organizer.Email
        };

        var updatedEventInfo = new UpdatedEventInfo
        {
            EventName = requestedEvent.Name,
            LocationName = requestedLocation.Name,
            ResponseDate = DateTime.Now
        };

        if(request.NewStatus == Domain.Enum.EventStatus.Approved)
        {
            await _emailService.SendApprovedEventNotification(receiverInfo, updatedEventInfo);
        }
        else
        {
            await _emailService.SendRejectedEventNotification(receiverInfo, updatedEventInfo);
        }

        return new UpdateEventStatusResponse { EventId = request.EventId, NewStatus = request.NewStatus };
    }
}
