namespace Service.Interfaces
{
    public interface IPassportRepository
    {
        Task Create();
        Task Update();
        Task GetById();
        Task GetAll();
        Task Delete();
    }
}
