using Application.Queries.GetUser;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<UserDto?> GetUserByGuid(Guid id);
    }
}
