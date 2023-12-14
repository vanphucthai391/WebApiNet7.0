using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
namespace WebApiNet7._0.Controllers
{
    public class NZWalkDbContext:DbContext
    {
        public NZWalkDbContext(DbContextOptions options):base(options){
            
        }
        public DbSet<Difficulty> Difficulties{get;set;}
        public DbSet<Region> Regions {get;set;}
        public DbSet<Walk> Walks{get;set;}

    }

}