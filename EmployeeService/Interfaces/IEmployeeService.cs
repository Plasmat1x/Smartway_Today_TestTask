using Service.Models;

namespace Service.Interfaces
{
    public interface IEmployeeService
    {
        Task<int> CreateEmployee(string name, string surname, string phone, int companyId, string departmentName, string departmentPhone, string passportType, string passportNumber);
        Task DeleteEmployee(int id);
        Task UpdateEmployee(int employeeId, string? name, string? surname, string? phone, int? companyId, string? departmentName, string? departmentPhone, string? passportType, string? passportNumber);
        Task<IEnumerable<Employee>> GetEmplyeesByCompanyDepartment(int companyId, string departmentName);
        Task<IEnumerable<Employee>> GetEmplyeesByCompany(int companyId);

    }
}
