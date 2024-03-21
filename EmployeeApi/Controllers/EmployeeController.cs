using Microsoft.AspNetCore.Mvc;

using Service.Interfaces;
using Service.Models;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController: ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(EmployeeCreateDTO model)
        {
            await employeeService.CreateEmployee(model);
            return Ok();
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await employeeService.DeleteEmployee(id);
            return Ok();
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(int employeeId, EmployeeUpdateDTO model)
        {
            await employeeService.UpdateEmployee(employeeId, model);
            return Ok();
        }

        [HttpPost("Get")]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}