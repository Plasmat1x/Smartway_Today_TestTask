using Service.Data.Entities;
using Service.Models;

namespace Service.Interfaces.Repositories
{
    public interface IDepartmentRepository
    {
        Task<int> Create(Department department);
        Task<DepartmentEntity> GetByName(string name);
        Task<DepartmentEntity> GetById(int id);
        Task Delete(int id);
        Task Update(Department department);
    }
}
