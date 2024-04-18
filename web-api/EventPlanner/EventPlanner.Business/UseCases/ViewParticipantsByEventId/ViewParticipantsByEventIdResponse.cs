using EventPlanner.Domain.Entities;

namespace EventPlanner.Business.UseCases.ViewParticipantsByEventId
{
    public sealed class ViewParticipantsByEventIdResponse
    {
        public IEnumerable<Participant> Participants { get; set; } = new List<Participant>();
    }
}
