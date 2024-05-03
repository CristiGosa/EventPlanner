using EventPlanner.Domain.Entities.Email;

namespace EventPlanner.Domain.Services
{
    public interface IEmailService
    {
        Task SendCreatedEventNotification(ReceiverInfo addresseeInfoDto, CreatedEventInfo createdTripEmailInfoDto);
    }
}
