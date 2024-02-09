using MediatR;
using System.Text.Json.Serialization;

namespace EventPlanner.Business.UseCases.ViewJoinedEvent
{
    public sealed class ViewJoinedEventRequest : IRequest<ViewJoinedEventResponse>
    {
        [JsonIgnore]
        public string AttendeeEmail { get; set; }
    }
}
