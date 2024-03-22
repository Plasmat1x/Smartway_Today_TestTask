using Service.Data.Entities;
using Service.Models;

namespace Service.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task<int> Create(Employee employee);
        Task Update(Employee employee);
        Task<IEnumerable<EmployeeEntity>> GetByCompany(int companyId);
        Task<IEnumerable<EmployeeEntity>> GetByCompanyDepartment(int companyId, string departmentName);
        Task<EmployeeEntity> GetById(int employeeId);
        Task Delete(int id);
    }
}
