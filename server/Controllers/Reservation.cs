using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ReservationController : ControllerBase
	{
		[HttpGet]
		public IActionResult GetReservations()
		{
			return Ok(new { message = "Get all reservations" });
		}
	}
}