using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	public class ReservationController : Controller
	{
		dynamic tables = new[]
		{
			new { Id = 1, Seat = 2, IsOccupied = false },
			new { Id = 2, Seat = 2, IsOccupied = false },
			new { Id = 3, Seat = 2, IsOccupied = false },
			new { Id = 4, Seat = 4, IsOccupied = false },
			new { Id = 5, Seat = 4, IsOccupied = false }
		};
		[HttpGet]
		public IActionResult GetReservations()
		{
			return Ok(new { message = "Get all reservations" });
		}

		[HttpGet]
		public IActionResult GetTables()
		{
			return Ok(new { message = tables });
		}
	}	
}