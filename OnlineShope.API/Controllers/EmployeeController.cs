using Microsoft.AspNetCore.Mvc;
using OnlineShope.Applicaition.Interfaces;
using OnlineShope.Applicaition.Models;

namespace OnlineShope.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService EmployeeService)
        {
            this.employeeService=employeeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await employeeService.Get(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await employeeService.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EmployeeDto model)
        {
            var result = await employeeService.Add(model);
            return Ok(result);
        }

        //[HttpPut]
        //public async Task<IActionResult> Edit(EmployeeDto model)
        //{
        //    var result = employeeService.Update(model);
        //    if (result.IsCompletedSuccessfully)
        //        return Ok(result);
        //    return BadRequest(result);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var result = employeeService.Delete(id);
        //    return Ok(result);
        //}
    }
}
