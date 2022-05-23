using Microsoft.AspNetCore.Mvc;
using OnlineShope.Applicaition.Interfaces;
using OnlineShope.Applicaition.Models;

namespace OnlineShope.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService=productService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result=await productService.Get(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result=await productService.GetAll();
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(ProductDto model)
        {
            var result= await productService.Add(model);
            return Ok(result);
        }

        //[HttpPut]
        //public async Task<IActionResult> Edit(ProductDto model)
        //{
        //    var result= productService.Update(model);
        //    if(result.IsCompletedSuccessfully)
        //        return Ok(result);
        //    return BadRequest(result);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var result = productService.Delete(id);
        //    return Ok(result);
        //}
    }
}
