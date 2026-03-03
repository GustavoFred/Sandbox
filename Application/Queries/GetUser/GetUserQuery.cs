using Domain.Models;
using MediatR;

namespace Application.Queries.GetUser
{
    public record GetUserQuery : IRequest<UserDto?>
    {
        public Guid Id { get; set; }
    }
}
