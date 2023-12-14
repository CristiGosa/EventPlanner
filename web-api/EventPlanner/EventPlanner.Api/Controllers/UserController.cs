using EventPlanner.Business.UseCases.Login;
using EventPlanner.Business.UseCases.RefreshToken;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace InVet.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
	private readonly IMediator _mediator;

	public UserController(IMediator mediator)
	{
		_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
	}

	[HttpPost("ExternalLogin")]
	public async Task<IActionResult> ExternalLoginAsync([FromBody] ExternalAuthenticationRequest request, CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(request, cancellationToken);

		return Ok(result);
	}
	
	[HttpPost("RefreshToken")]
	public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(request, cancellationToken);

		return Ok(result);
	}
}