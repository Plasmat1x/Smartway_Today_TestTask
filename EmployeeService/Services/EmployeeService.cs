using Service.Interfaces;
using Service.Models;

namespace Service.Services
{
    public class EmployeeService: IEmployeeService
    {
        public Task<int> CreateEmployee(EmployeeCreateDTO employee)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetEmplyeesByCompany(int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetEmplyeesByCompanyDepartment(int companyId, string departmentName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEmployee(int employeeId, EmployeeUpdateDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
