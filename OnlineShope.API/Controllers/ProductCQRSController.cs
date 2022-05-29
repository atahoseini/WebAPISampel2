using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShope.Applicaition.CQRS.ProductCommandQuery.Command;

namespace OnlineShope.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductCQRSController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductCQRSController(IMediator mediator)
        {
            this.mediator=mediator;
        }
        //[HttpPost]
        //public async Task<IActionResult> Create(SaveProductCommand saveProductCommand)
        //{
        //    var result=await mediator.Send(saveProductCommand);
        //}
    }
}
