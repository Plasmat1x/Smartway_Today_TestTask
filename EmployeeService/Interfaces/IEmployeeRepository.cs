using Service.Models;

namespace Service.Interfaces
{
    public interface IEmployeeRepository
    {
        Task Create();
        Task Update(int employeeId, EmployeeUpdateDTO model);
        Task<IEnumerable<Employee>> GetByCompany(int companyId);
        Task<IEnumerable<Employee>> GetByCompanyDepartment(int companyId, string departmentName);
        Task Delete();
    }
}
