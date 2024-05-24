using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("/[controller]")]
public class ReservationController: ControllerBase
{
    [HttpGet("Hello")]
    public async Task<string> Hello()
    {
        return "Hello World!";
    } 
}