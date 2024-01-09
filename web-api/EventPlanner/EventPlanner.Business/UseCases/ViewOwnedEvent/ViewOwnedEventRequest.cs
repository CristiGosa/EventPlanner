using MediatR;
using System.Text.Json.Serialization;

namespace EventPlanner.Business.UseCases.ViewOwnedEvent
{
    public sealed class ViewOwnedEventRequest : IRequest<ViewOwnedEventResponse>
    {
        [JsonIgnore]
        public string OrganizerEmail { get; set; }
    }
}
