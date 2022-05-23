using Microsoft.AspNetCore.Mvc;
using OnlineShope.Applicaition.Interfaces;
using OnlineShope.Applicaition.Models;

namespace OnlineShope.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService=customerService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await customerService.Get(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await customerService.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CustomerDto model)
        {
            var result = await customerService.Add(model);
            return Ok(result);
        }

        //[HttpPost]
        //public async Task<IActionResult> Edit(CustomerDto model)
        //{
        //    var result = customerService.Update(model);
        //    if (result.IsCompletedSuccessfully)
        //        return Ok(result);
        //    return BadRequest(result);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var result = customerService.Delete(id);
        //    return Ok(result);
        //}

    }
    
}
