using Service.Interfaces;
using Service.Models;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository: IEmployeeRepository
    {
        public Task Create()
        {
            throw new NotImplementedException();
        }

        public Task Delete()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetByCompany(int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetByCompanyDepartment(int companyId, string departmentName)
        {
            throw new NotImplementedException();
        }

        public Task Update(int employeeId, EmployeeUpdateDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
