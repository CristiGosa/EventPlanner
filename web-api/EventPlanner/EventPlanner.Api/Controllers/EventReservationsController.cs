using EventPlanner.Business.UseCases.CreateEventReservation;
using EventPlanner.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventPlanner.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventReservationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventReservationsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateEventReservationRequest request, CancellationToken cancellationToken)
        {
            request.AttendeeEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);

            EventReservation result = await _mediator.Send(request, cancellationToken);

            return Created(string.Empty, result);
        }
    }
}
