using Microsoft.EntityFrameworkCore;

namespace WebApiNet7._0.Controllers
{
public class NpgSQLRepository:IRegionRepository
{
    private readonly NZWalkDbContext dbContext;
    public NpgSQLRepository(NZWalkDbContext dbContext){
        this.dbContext=dbContext;
    }
    public async Task <List<Region>>GetAllAsync(){
        return await dbContext.Regions.ToListAsync();
    }
}
}