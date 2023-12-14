using EventPlanner.Domain.Enum;
using EventPlanner.Domain.Exceptions;
using EventPlanner.Domain.Repositories;
using EventPlanner.Domain.Services;

using MediatR;

namespace EventPlanner.Business.UseCases.RefreshToken;
public sealed class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, RefreshTokenResponse>
{
	private readonly IAuthenticationService _authenticationService;
	private readonly IUnitOfWork _unitOfWork;

	public RefreshTokenHandler(IAuthenticationService authenticationService, IUnitOfWork unitOfWork)
	{
		_authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
		_unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
	}

	public async Task<RefreshTokenResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
	{
		var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);

		if (user.RefreshToken != request.OldRefreshToken)
		{
			throw new InvalidRefreshTokenException();
		}

		if (user.RefreshTokenExpire < DateTime.Now)
		{
			throw new ExpiredRefreshTokenException();
		}

		var newRefreshToken = _authenticationService.GenerateRefreshToken();

		user.RefreshToken = newRefreshToken.Token;
		user.RefreshTokenExpire = newRefreshToken.Expires;
		await _unitOfWork.Users.UpdateUserAsync(user);
		await _unitOfWork.CommitAsync(cancellationToken);

		var roles = await _unitOfWork.Users.GetRolesAsync(user);

		if (roles == null || !roles.Any())
		{
			throw new NotFoundException(typeof(Role));
		}

		var token = _authenticationService.GenerateToken(user, roles);

		var response = new RefreshTokenResponse() { Token = token, RefreshToken = newRefreshToken.Token };

		return response;
	}
}
