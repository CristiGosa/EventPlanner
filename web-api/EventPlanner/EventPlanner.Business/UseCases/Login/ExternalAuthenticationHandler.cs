using AutoMapper;

using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Enum;
using EventPlanner.Domain.Options;
using EventPlanner.Domain.Repositories;
using EventPlanner.Domain.Services;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace EventPlanner.Business.UseCases.Login;
public sealed class ExternalAuthenticationHandler : IRequestHandler<ExternalAuthenticationRequest, ExternalAuthenticationResponse>
{
	private readonly IAuthenticationService _authenticationService;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	private readonly InitialUsers _options;

	public ExternalAuthenticationHandler(IAuthenticationService authenticationService, IMapper mapper, IUnitOfWork unitOfWork, IOptions<InitialUsers> options)
	{
		_authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
		_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		_unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		_options = options.Value ?? throw new ArgumentNullException(nameof(options));
	}

	public async Task<ExternalAuthenticationResponse> Handle(ExternalAuthenticationRequest request, CancellationToken cancellationToken)
	{
		var userInfo = await _authenticationService.VerifyGoogleTokenAsync(request.IdToken);

		var loginInfo = new UserLoginInfo(userInfo.LoginProvider, userInfo.ProviderKey, userInfo.LoginProvider);

		var user = await _unitOfWork.Users.GetByLoginAsync(loginInfo.LoginProvider, loginInfo.ProviderKey);

		if (user == null)
		{
			user = _mapper.Map<User>(userInfo);

			await _unitOfWork.Users.RegisterExternalAsync(user);
			await _unitOfWork.Users.AddLoginAsync(user, loginInfo);
			await _unitOfWork.Users.AddRoleAsync(user, Role.User.ToString());
			if (user.Email == _options.AdminEmail)
			{
				await _unitOfWork.Users.AddRoleAsync(user, Role.Admin.ToString());
			}
			if (user.Email == _options.OrganizerEmail)
			{
				await _unitOfWork.Users.AddRoleAsync(user, Role.Organizer.ToString());
			}
		}

		var refreshToken = _authenticationService.GenerateRefreshToken();
		user.RefreshToken = refreshToken.Token;
		user.RefreshTokenExpire = refreshToken.Expires;
		await _unitOfWork.Users.UpdateUserAsync(user);

		await _unitOfWork.CommitAsync(cancellationToken);

		IList<string> roles = await _unitOfWork.Users.GetRolesAsync(user);

		var token = _authenticationService.GenerateToken(user, roles);

		return new ExternalAuthenticationResponse() { Token = token, Roles = roles, RefreshToken = refreshToken.Token };
	}
}
