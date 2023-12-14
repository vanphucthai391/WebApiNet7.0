using Microsoft.AspNetCore.Mvc;

namespace WebApiNet7._0.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllStudent(){
        string[] studentNames= new string []{"John","Jane", "Mark", "Emily","David"};

        return Ok(studentNames);
    }

}
