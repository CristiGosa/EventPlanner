using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Repositories;

using Microsoft.AspNetCore.Identity;

namespace EventPlanner.Database.Repositories;

internal sealed class UserRepository : IUserRepository
{
	private readonly UserManager<User> _userManager;

	public UserRepository(UserManager<User> userManager)
	{
		_userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
	}

	public async Task RegisterExternalAsync(User user)
	{
		await _userManager.CreateAsync(user);
	}

	public async Task<User> GetByLoginAsync(string loginProvider, string providerKey)
	{
		return await _userManager.FindByLoginAsync(loginProvider, providerKey);
	}
	public async Task<User> GetByEmailAsync(string email)
	{
		return await _userManager.FindByEmailAsync(email);
	}

	public async Task AddLoginAsync(User user, UserLoginInfo loginInfo)
	{
		await _userManager.AddLoginAsync(user, loginInfo);
	}

	public async Task UpdateUserAsync(User user)
	{
		await _userManager.UpdateAsync(user);
	}

	public async Task AddRoleAsync(User user, string role)
	{
		await _userManager.AddToRoleAsync(user, role);
	}

	public async Task<IList<string>> GetRolesAsync(User user)
	{
		return await _userManager.GetRolesAsync(user);
	}
}