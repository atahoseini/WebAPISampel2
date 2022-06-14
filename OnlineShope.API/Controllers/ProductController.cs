using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShope.API.CustomAttributes;
using OnlineShope.Applicaition.Interfaces;
using OnlineShope.Applicaition.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace OnlineShope.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService=productService;
        }
        [HttpGet("{id}")]
        //[SwaggerOperation(
        //  Summary = "Get a Product",
        //  Description = "Get a Product with id",
        //  OperationId = "Products.Get",
        //  Tags = new[] { "ProductController" })
        //]

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
        //[AccessControl( Permission ="product-add")]
        [AllowAnonymous]

        public async Task<IActionResult> Create([FromForm]ProductDto model)
        {
            var result= await productService.Add(model);
            return Ok(result);
        }


        [HttpPost("upload")]
        [AllowAnonymous]
        public async Task<IActionResult> Upload(IFormFile thumbnail)
        {
            //1-save to byte
            //1-save to byte[]
            using (var target = new MemoryStream())
            {
                thumbnail.CopyTo(target);
                var thumbnailByteArray = target.ToArray();
            }

            //2-save in folders
            string filePath = @"E:\";
            FileInfo fileInfo = new FileInfo(thumbnail.FileName);
            string fileName = thumbnail.FileName + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(filePath, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                thumbnail.CopyTo(stream);
            }
            return Ok();
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
