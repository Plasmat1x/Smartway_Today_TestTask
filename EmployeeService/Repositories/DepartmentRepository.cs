using Dapper;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using Service.Data.Entities;
using Service.Interfaces.Repositories;
using Service.Models;

using System.Data;

namespace Service.Repositories
{
    public class DepartmentRepository: IDepartmentRepository
    {
        private readonly string connectionString;

        public DepartmentRepository(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("DapperConnection");
        }

        public async Task<int> Create(Department department)
        {
            using(IDbConnection db = new SqlConnection(connectionString))
            {
                var departmentEntity = new DepartmentEntity {
                    Name = department.Name,
                    Phone = department.Phone,
                };

                var sqlQuery = "INSERT INTO Departments (Name, Phone) VALUES(@Name, @Phone); SELECT CAST(SCOPE_IDENTITY() as int); ";

                int? departmentId = await db.QueryFirstOrDefaultAsync<int>(sqlQuery, departmentEntity);
                return departmentId.Value;
            }
        }

        public async Task Delete(int id)
        {
            using(IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Departments WHERE Id = @Id; ";

                await db.ExecuteAsync(sqlQuery, new { id });
            }
        }

        public async Task<DepartmentEntity> GetById(int id)
        {
            using(IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"SELECT * FROM Departments WHERE Id = {id};";

                var department = await db.QueryFirstOrDefaultAsync<DepartmentEntity>(sqlQuery, new { id });
                return department;
            }
        }

        public async Task<DepartmentEntity> GetByName(string name)
        {
            using(IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "SELECT * FROM Departments WHERE Name = @Name; ";

                var department = await db.QueryFirstOrDefaultAsync<DepartmentEntity>(sqlQuery, new { name });
                return department;
            }
        }

        public async Task Update(Department department)
        {
            using(IDbConnection db = new SqlConnection(connectionString))
            {
                var departmentEntity = await GetByName(department.Name);

                if(!department.Name.Equals(departmentEntity.Name))
                {
                    string sqlQuery = $"UPDATE Departments SET Name = @Name WHERE Id = {departmentEntity.Id}; ";
                    await db.ExecuteAsync(sqlQuery, new { department.Name });
                }

                if(!department.Phone.Equals(departmentEntity.Phone))
                {
                    string sqlQuery = $"UPDATE Departments SET Phone = @Phone WHERE Id = {departmentEntity.Id}; ";
                    await db.ExecuteAsync(sqlQuery, new { department.Phone });
                }
            }
        }
    }
}
