using EventPlanner.Business.UseCases.CreateLocation;
using EventPlanner.Business.UseCases.ViewLocation;
using EventPlanner.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LocationController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateLocationRequest request, CancellationToken cancellationToken)
        {
            Location result = await _mediator.Send(request, cancellationToken);

            return Created(string.Empty, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ViewLocationRequest(), cancellationToken);

            return Ok(result);
        }
    }
}
