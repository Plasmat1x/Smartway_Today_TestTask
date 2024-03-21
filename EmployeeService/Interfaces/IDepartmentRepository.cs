namespace Service.Interfaces
{
    public interface IDepartmentRepository
    {
        Task Create();
        Task Update();
        Task GetById();
        Task GetAll();
        Task Delete();
    }
}
