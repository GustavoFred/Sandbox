using Application.Commands.CreateUser;
using Application.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator) 
        {
            _mediator = mediator;   
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<ActionResult<Guid>> CreateUserAsync(CreateUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        [Route("GetUserByGuid")]
        public async Task<ActionResult<UserDto>> GetUserByGuidAsync([FromQuery] GetUserQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
