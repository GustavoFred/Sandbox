using Application.Interfaces;
using MediatR;

namespace Application.Queries.GetUser
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, UserDto?>
    {
        private readonly IUserRepository _userRepository;

        public GetUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto?> Handle(GetUserQuery request, CancellationToken token)
        {
            var user = await _userRepository.GetUserByGuid(request.Id);

            return user;
        }
    }
}
