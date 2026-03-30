using MediatR;

namespace Application.Commands.CreateUser
{
    public record CreateUserCommand : IRequest<Guid>
    {
        public string Name { get; set; } = null!;
        public string SecondName { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
