using Microsoft.AspNetCore.Mvc;

namespace SendMailExample.Controllers;

[ApiController]
[Route("[controller]")]
public class MailController : ControllerBase
{
    [HttpPost]
    public IActionResult Send(string toAddress)
    {
        Mailer m = new Mailer();
        m.SendEmail(toAddress);
        return Ok("Success");
    }
}