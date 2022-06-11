using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShope.Applicaition.CQRS.AuthenticateCommandQuery.Command;

namespace OnlineShope.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthenticateController(IMediator mediator)
        {
            this.mediator=mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Post(LoginCommand loginCommand)
        {
            var result=await mediator.Send(loginCommand);
            return Ok(result);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand registerCommand)
        {
            var result = await mediator.Send(registerCommand);
            return Ok(result);
        }
    }
}
