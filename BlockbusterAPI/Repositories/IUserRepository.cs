using BlockbusterAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockbusterAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(Guid id);
        Task<IEnumerable<User>> GetUsersAsync();
        Task CreateUserAsync(User user);
        Task DeleteUserAsync(Guid id);
        Task UpdateUserAsync(User user);
    }
}
