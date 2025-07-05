using UserLoginApi.Models;

namespace UserLoginApi.Interfaces
{
    public interface IUserRepository
    {
        Task<User> RegisterUserAsync(string username, string password);
        Task<string> LoginUserAsync(string username, string password);
        Task<bool> UserExistsAsync(string username);
    }
}