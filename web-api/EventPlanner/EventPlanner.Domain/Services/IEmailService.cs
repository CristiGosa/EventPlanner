using EventPlanner.Domain.Entities.Email;

namespace EventPlanner.Domain.Services
{
    public interface IEmailService
    {
        Task SendCreatedEventNotification(CreatedEventInfo createdTripEmailInfoDto);
        Task SendApprovedEventNotification(ReceiverInfo receiverInfo, UpdatedEventInfo updatedEventInfo);
        Task SendRejectedEventNotification(ReceiverInfo receiverInfo, UpdatedEventInfo updatedEventInfo);
    }
}
