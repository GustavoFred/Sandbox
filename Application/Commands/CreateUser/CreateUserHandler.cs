using Application.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        public readonly IUserRepository _repository;

        public CreateUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken token)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                SecondName = request.SecondName,
                Password = "a", //As of now
                UserGuid = Guid.NewGuid(),
                PersonType = false
            };

            await _repository.AddAsync(user);
            
            return user.UserGuid;
        }
    }
}
