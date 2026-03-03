using Application.Interfaces;
using Application.Queries.GetUser;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SandBoxContext _context;
        public UserRepository(SandBoxContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserDto?> GetUserByGuid(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserGuid == id);

            if (user != null) {
                UserDto userDto = new()
                {
                    UserGuid = id,
                    Name = user.Name,
                    SecondName = user.SecondName,
                    Email = user.Email,
                };
                return userDto;
            }

            throw new Exception("User not found");
        }
    }
}
