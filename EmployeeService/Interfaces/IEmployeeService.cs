using Service.Models;

namespace Service.Interfaces
{
    public interface IEmployeeService
    {
        Task<int> CreateEmployee(EmployeeCreateDTO employee);
        Task DeleteEmployee(int id);
        Task UpdateEmployee(int employeeId, EmployeeUpdateDTO model);
        Task<IEnumerable<Employee>> GetEmplyeesByCompanyDepartment(int companyId, string departmentName);
        Task<IEnumerable<Employee>> GetEmplyeesByCompany(int companyId);
    }
}
