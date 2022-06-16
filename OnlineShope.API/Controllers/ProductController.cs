using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OnlineShope.API.CustomAttributes;
using OnlineShope.Applicaition.Interfaces;
using OnlineShope.Applicaition.Models;
using StackExchange.Profiling;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.AccessControl;
using System.Security.Cryptography;


namespace OnlineShope.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("MyAPI")] //=> گفتگوی چند دامنه و دسترسی ها
    //[Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IWebHostEnvironment environment;
        private readonly IConfiguration configuration;

        public ProductController(IProductService productService, IWebHostEnvironment environment,
            IConfiguration configuration)
        {
            this.productService = productService;
            this.environment = environment;
            this.configuration = configuration;
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
        public async Task<IActionResult> GetAll(int page = 1, int size = 3)
        {
            using (MiniProfiler.Current.Step("Product GetAll Controller"))
            {
                var result = await productService.GetAll(page, size);
                return Ok(result);
            }
        }


        [HttpGet("GetFileContent")]
        [AllowAnonymous]
        public async Task<FileContentResult> GetFileContent(string fileUrl)
        {
            var urlSections = fileUrl.Split("/");
            //read file and decrypt content
            byte[] encryptedData = await System.IO.File.ReadAllBytesAsync("");
            var decryptedData = Decrypt(encryptedData);

            return new FileContentResult(decryptedData, "application/txt");
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
        private byte[] Encrypt(byte[] fileContent)
        {
            string EncryptionKey = "MAKV2SPBNI54324";
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(fileContent, 0, fileContent.Length);
                        cryptoStream.FlushFinalBlock();
                        return memoryStream.ToArray();
                    }
                }
            }
        }

        private byte[] Decrypt(byte[] fileContent)
        {
            string EncryptionKey = "MAKV2SPBNI54324";
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);


                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(fileContent, 0, fileContent.Length);
                        cryptoStream.FlushFinalBlock();
                        return memoryStream.ToArray();
                    }
                }
            }
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
