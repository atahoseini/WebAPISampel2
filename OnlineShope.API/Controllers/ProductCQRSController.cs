using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShope.Applicaition.CQRS.ProductCommandQuery.Command;
using OnlineShope.Applicaition.CQRS.ProductCommandQuery.Query;

namespace OnlineShope.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]

    public class ProductCQRSController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductCQRSController(IMediator mediator)
        {
            this.mediator=mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveProductCommand saveProductCommand)
        {
            var result = await mediator.Send(saveProductCommand);
            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> Get([FromQuery]GetProductQuery getProductQuery)
        {
            var result=await mediator.Send(getProductQuery);
            return Ok(result);
        }
    }
}
