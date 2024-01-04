using EventPlanner.Domain.Entities;

using Microsoft.AspNetCore.Identity;

namespace EventPlanner.Domain.Repositories;

public interface IUserRepository
{
	Task RegisterExternalAsync(User user);
	Task<User> GetByLoginAsync(string loginProvider, string providerKey);
    Task<User> GetByEmailAsync(string email);
	Task AddLoginAsync(User user, UserLoginInfo loginInfo);
	Task AddRoleAsync(User user, string role);
	Task<IList<string>> GetRolesAsync(User user);
	Task UpdateUserAsync(User user);
}