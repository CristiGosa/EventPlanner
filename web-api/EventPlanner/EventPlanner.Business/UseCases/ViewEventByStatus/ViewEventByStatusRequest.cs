using EventPlanner.Domain.Enum;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Business.UseCases.ViewEventByStatus
{
    public sealed class ViewEventByStatusRequest : IRequest<ViewEventByStatusResponse>
    {
        [Required]
        public EventStatus Status { get; set; }
    }
}
