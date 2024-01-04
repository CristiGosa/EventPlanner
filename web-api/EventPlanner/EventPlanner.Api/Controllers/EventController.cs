using EventPlanner.Business.UseCases.CreateEvent;
using EventPlanner.Business.UseCases.ViewEvent;
using EventPlanner.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventPlanner.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateEventRequest request, CancellationToken cancellationToken)
        {
            request.OrganizerEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);

            Event result = await _mediator.Send(request, cancellationToken);

            return Created(string.Empty, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ViewEventRequest(), cancellationToken);

            return Ok(result);
        }
    }
}
