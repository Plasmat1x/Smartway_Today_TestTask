using Service.Interfaces;
using Service.Interfaces.Repositories;
using Service.Models;

namespace Service.Services
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IDepartmentRepository departmentRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            this.employeeRepository = employeeRepository;
            this.departmentRepository = departmentRepository;
        }

        public async Task<int> CreateEmployee(string name, string surname, string phone, int companyId, string departmentName, string departmentPhone, string passportType, string passportNumber)
        {
            var passport = Passport.Create(passportType, passportNumber);
            var departmentEntity = await departmentRepository.GetByName(departmentName);
            if(departmentEntity == null)
            {
                var departmentId = await departmentRepository.Create(Department.Create(departmentName, departmentPhone));
                departmentEntity = await departmentRepository.GetById(departmentId);
            }

            var department = Department.Create(departmentEntity.Name, departmentEntity.Phone);

            var employee = Employee.Create(name, surname, phone, companyId, department, passport);

            await employeeRepository.Create(employee);

            return employee.Id;
        }

        public async Task DeleteEmployee(int id)
        {
            await employeeRepository.Delete(id);
        }

        public async Task<IEnumerable<Employee>> GetEmplyeesByCompany(int companyId)
        {
            var entities = await employeeRepository.GetByCompany(companyId);
            List<Employee> result = new List<Employee>();
            foreach(var entity in entities)
            {
                var departmentEntity = await departmentRepository.GetById(entity.DepartmentId);
                var department = Department.Create(departmentEntity.Name, departmentEntity.Phone);
                var employee = Employee.Create(entity.Name, entity.Surname, entity.Phone, entity.CompanyId, department, Passport.Create(entity.PassportType, entity.PassportNumber));
                employee.UpdateId(entity.Id);
                result.Add(employee);
            }

            return result;
        }

        public async Task<IEnumerable<Employee>> GetEmplyeesByCompanyDepartment(int companyId, string departmentName)
        {
            var entities = await employeeRepository.GetByCompanyDepartment(companyId, departmentName);

            List<Employee> result = new List<Employee>();
            foreach(var entity in entities)
            {
                var departmentEntity = await departmentRepository.GetById(entity.DepartmentId);
                var department = Department.Create(departmentEntity.Name, departmentEntity.Phone);
                var employee = Employee.Create(entity.Name, entity.Surname, entity.Phone, entity.CompanyId, department, Passport.Create(entity.PassportType, entity.PassportNumber));
                employee.UpdateId(entity.Id);
                result.Add(employee);
            }

            return result;
        }

        public async Task UpdateEmployee(int employeeId, string? name, string? surname, string? phone, int? companyId, string? departmentName, string? departmentPhone, string? passportType, string? passportNumber)
        {
            var employeeEntity = await employeeRepository.GetById(employeeId);
            var departmentEntity = await departmentRepository.GetById(employeeEntity.DepartmentId);

            Department department = null;
            int departmentId;
            if(departmentEntity != null)
            {
                department = Department.Create(departmentEntity.Name, departmentEntity.Phone);

                if(departmentName != null)
                {
                    department.UpdateName(departmentName);
                    await departmentRepository.Update(department);
                }

                if(departmentPhone != null)
                {
                    department.UpdatePhone(departmentPhone);
                    await departmentRepository.Update(department);
                }
            }
            else
            {
                if(departmentName != null && departmentPhone != null)
                {
                    departmentId = await departmentRepository.Create(Department.Create(departmentName, departmentPhone));
                }
            }

            var employee = Employee.Create(employeeEntity.Name, employeeEntity.Surname, employeeEntity.Phone, employeeEntity.CompanyId, department, Passport.Create(employeeEntity.PassportType, employeeEntity.PassportNumber));

            employee.UpdateId(employeeEntity.Id);

            if(name != null)
            {
                employee.UpdateName(name);
            }

            if(surname != null)
            {
                employee.UpdateSurname(surname);
            }

            if(phone != null)
            {
                employee.UpdatePhone(phone);
            }

            if(companyId != null && companyId.Value > 0)
            {
                employee.UpdateCompanyId(companyId.Value);
            }

            if(passportType != null)
            {
                employee.Passport.UpdateType(passportType);
            }

            if(passportNumber != null)
            {
                employee.Passport.UpdateNumber(passportNumber);
            }

            await employeeRepository.Update(employee);
        }
    }
}
