namespace WebApiNet7._0.Controllers
{
    public interface IRegionRepository{
        Task <List<Region>> GetAllAsync();
    }
}