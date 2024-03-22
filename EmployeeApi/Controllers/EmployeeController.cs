using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using Service.DTO;
using Service.Interfaces;

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
            int res = await employeeService.CreateEmployee(model.Name, model.Surname, model.Phone, model.CompanyId, model.DepartmentName, model.DepartmentPhone, model.PassportType, model.PassportNumber);
            return Ok(res);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await employeeService.DeleteEmployee(id);
            return Ok();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int employeeId, EmployeeUpdateDTO model)
        {
            await employeeService.UpdateEmployee(employeeId, model.Name, model.Surname, model.Phone, model.CompanyId, model.DepartmentName, model.DepartmentPhone, model.PassportType, model.PassportNumber);
            return Ok();
        }

        [HttpGet("GetByCompany")]
        public async Task<IActionResult> GetByCompany(int companyId)
        {
            var res = await employeeService.GetEmplyeesByCompany(companyId);

            if(res.IsNullOrEmpty())
            {
                return NotFound("is null or empty");
            }
            return Ok(res);
        }

        [HttpGet("GetByCompanyDepartment")]
        public async Task<IActionResult> GetByCompanyDepartment(int companyId, string departmentName)
        {
            var res = await employeeService.GetEmplyeesByCompanyDepartment(companyId, departmentName);

            return Ok(res);
        }
    }
}