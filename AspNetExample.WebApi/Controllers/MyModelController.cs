using AspNetExample.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetExample.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MyModelController : ControllerBase
{
    private readonly IService _service;

    public MyModelController(IService service)
    {
        _service = service;
    }

    [HttpGet("GetModel/{data}")]
    public IActionResult Get(string data) => Ok(_service.GetModel(data));
}