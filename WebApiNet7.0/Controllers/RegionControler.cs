using System.Security.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace WebApiNet7._0.Controllers;

[ApiController]
[Route("[controller]")]
public class RegionController : ControllerBase
{
    private readonly NZWalkDbContext dbContext;
    public RegionController(NZWalkDbContext dbContext){
        this.dbContext=dbContext;
    }
    [HttpGet]
    public IActionResult GetAll(){
        /*
        #region Feed Data
                  var regions=new List<Region>
        {
            new Region{
                Id=Guid.NewGuid(),
                Name="Australia Region",
                Code="AUS",
                RegionImageUrl="https://images.pexels.com/photos/19407605/pexels-photo-19407605/free-photo-of-sa-m-c-brasileiro.jpeg"
            },
            new Region{
                Id=Guid.NewGuid(),
                Name="Asia",
                Code="ASI",
                RegionImageUrl="https://images.pexels.com/photos/19872/pexels-photo.jpg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
            }
        };  
        #endregion
        */       
        var regions=dbContext.Regions.ToList();
        var regionsDto=new List<RegionDto>();
        foreach(var region in regions){
            regionsDto.Add(
                new RegionDto(){Id=region.Id,Code=region.Code,Name=region.Name,RegionImageUrl=region.RegionImageUrl});
        };
        return Ok(regionsDto);
    }
    [HttpGet]
    [Route("{id:Guid}")]
    public IActionResult GetById([FromRoute] Guid id){
        var region=dbContext.Regions.FirstOrDefault(x=>x.Id==id);
        var regionDto=new RegionDto()
        {
            Id=region.Id,Code=region.Code,Name=region.Name,RegionImageUrl=region.RegionImageUrl
        };
        if(region==null){
            return NotFound();
        }
        return Ok(regionDto);
    }
    [HttpPost]
    public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto){
        //Map or Convert DTO to Domain model
        var regionDomainModel=new Region{
            Code=addRegionRequestDto.Code,Name=addRegionRequestDto.Name,RegionImageUrl=addRegionRequestDto.RegionImageUrl
        };
        //Use domain model to create Region
        dbContext.Regions.Add(regionDomainModel);
        dbContext.SaveChanges();
        //Map domain mdoel back to DTO
        var regionDto=new RegionDto{
            Id=regionDomainModel.Id,
            Code=regionDomainModel.Code,
            Name=regionDomainModel.Name,
            RegionImageUrl=regionDomainModel.RegionImageUrl
        };
        return CreatedAtAction(nameof(GetById),new {id=regionDto.Id},regionDto);
    }
    [HttpPut]
    [Route("{id:Guid}")]
    public IActionResult Update([FromRoute] Guid id,[FromBody] UpdateRegionRequestDto updateRegionRequestDto){
    //Check if region exits
    var regionDomainModel=dbContext.Regions.FirstOrDefault(x=>x.Id==id);
    if(regionDomainModel==null){
        return NotFound();
    }
    regionDomainModel.Code=updateRegionRequestDto.Code;
    regionDomainModel.Name=updateRegionRequestDto.Name;
    regionDomainModel.RegionImageUrl=updateRegionRequestDto.RegionImageUrl;
    dbContext.SaveChanges();
    //Map domain mdoel back to DTO
    var regionDto=new RegionDto{
        Id=regionDomainModel.Id,
        Code=regionDomainModel.Code,
        Name=regionDomainModel.Name,
        RegionImageUrl=regionDomainModel.RegionImageUrl
    };
    return Ok(regionDto);
    }
    [HttpDelete]
    [Route("{id:Guid}")]
    public IActionResult Delete([FromRoute] Guid id){
    var regionDomainModel=dbContext.Regions.FirstOrDefault(x=>x.Id==id);
    if(regionDomainModel==null)
        return NotFound();
    dbContext.Regions.Remove(regionDomainModel);
    dbContext.SaveChanges();
        //Map domain mdoel back to DTO
    var regionDto=new RegionDto{
        Id=regionDomainModel.Id,
        Code=regionDomainModel.Code,
        Name=regionDomainModel.Name,
        RegionImageUrl=regionDomainModel.RegionImageUrl
    };
    return Ok(regionDto);
    }
}
