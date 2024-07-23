using ItransitionAPI.Models;

namespace ItransitionAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task UpdateUserStatusAsync(int id, string status);
        Task<User> GetUserByEmailAndPasswordAsync(string email, string password);

    }
}
