using Dapper;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using Service.Data.Entities;
using Service.Interfaces.Repositories;
using Service.Models;

using System.Data;

namespace Service.Repositories
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly string connectionString;

        public EmployeeRepository(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("DapperConnection");

        }

        public async Task<int> Create(Employee employee)
        {
            using(IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "SELECT Id FROM Departments WHERE Name = @Name; ";

                int? departmentId = await db.QueryFirstOrDefaultAsync<int>(sqlQuery, new { employee.Department.Name });

                if(departmentId == null)
                {
                    return 0;
                }

                var employeeEntity = new EmployeeEntity {
                    Name = employee.Name,
                    Surname = employee.Surname,
                    Phone = employee.Phone,
                    PassportType = employee.Passport.Type,
                    PassportNumber = employee.Passport.Number,
                    CompanyId = employee.CompanyId,
                    DepartmentId = departmentId.Value,
                };

                sqlQuery = "INSERT INTO Employees (Name, Surname, Phone, PassportType, PassportNumber, CompanyId, DepartmentId) VALUES(@Name, @Surname, @Phone, @PassportType, @PassportNumber, @CompanyId, @DepartmentId); SELECT CAST(SCOPE_IDENTITY() as int); ";

                int? employeeId = await db.QueryFirstOrDefaultAsync<int>(sqlQuery, employeeEntity);
                employee.UpdateId(employeeId.Value);
                return employeeId.Value;
            }
        }

        public async Task Delete(int id)
        {
            using(IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Employees WHERE Id = @id; ";
                await db.ExecuteAsync(sqlQuery, new { id });
            }
        }

        public async Task<IEnumerable<EmployeeEntity>> GetByCompany(int companyId)
        {
            using(IDbConnection db = new SqlConnection(connectionString))
            {

                var sqlQuery = "SELECT * FROM Employees WHERE CompanyId = @CompanyId; ";
                IEnumerable<EmployeeEntity> entities = await db.QueryAsync<EmployeeEntity>(sqlQuery, new { companyId });

                return entities;
            }
        }

        public async Task<IEnumerable<EmployeeEntity>> GetByCompanyDepartment(int companyId, string departmentName)
        {
            using(IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "SELECT * FROM Employees WHERE CompanyId = @CompanyId AND DepartmentId = (SELECT Id FROM Departments WHERE Name = @DepartmentName); ";
                IEnumerable<EmployeeEntity> entities = await db.QueryAsync<EmployeeEntity>(sqlQuery, new { companyId, departmentName });

                return entities;
            }
        }

        public async Task<EmployeeEntity> GetById(int employeeId)
        {
            using(IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"SELECT * FROM Employees WHERE Id = {employeeId};";

                var employee = await db.QueryFirstOrDefaultAsync<EmployeeEntity>(sqlQuery, new { employeeId });

                return employee;
            }
        }

        public async Task Update(Employee employee)
        {
            if(employee.Id == default)
            {
                return;
            }

            using(IDbConnection db = new SqlConnection(connectionString))
            {

                var employeeEntity = await GetById(employee.Id);

                if(!employee.Name.Equals(employeeEntity.Name))
                {
                    string sqlQuery = $"UPDATE Employees SET Name = @Name WHERE Id = {employee.Id}; ";
                    await db.ExecuteScalarAsync(sqlQuery, new { employee.Name });
                }

                if(!employee.Surname.Equals(employeeEntity.Surname))
                {
                    string sqlQuery = $"UPDATE Employees SET Surname = @Surname WHERE Id = {employee.Id}; ";
                    await db.ExecuteScalarAsync(sqlQuery, new { employee.Surname });
                }

                if(employee.CompanyId != employeeEntity.CompanyId)
                {
                    string sqlQuery = $"UPDATE Employees SET CompanyId = {employee.CompanyId} WHERE Id = {employee.Id}; ";
                    await db.ExecuteScalarAsync(sqlQuery);
                }

                if(!employee.Phone.Equals(employeeEntity.Phone))
                {
                    string sqlQuery = $"UPDATE Employees SET Phone = @Phone WHERE Id = {employee.Id}; ";
                    await db.ExecuteScalarAsync(sqlQuery, new { employee.Phone });
                }

                if(!employee.Passport.Type.Equals(employeeEntity.PassportType))
                {
                    string sqlQuery = $"UPDATE Employees SET PassportType = @PassportType WHERE Id = {employee.Id}; ";
                    await db.ExecuteScalarAsync(sqlQuery, new { employee.Passport.Type });
                }

                if(!employee.Passport.Number.Equals(employeeEntity.PassportNumber))
                {
                    string sqlQuery = $"UPDATE Employees SET PassportNumber = @PassportNumber WHERE Id = {employee.Id}; ";
                    await db.ExecuteScalarAsync(sqlQuery, new { employee.Passport.Number });
                }
            }
        }
    }
}