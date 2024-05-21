using Microsoft.AspNetCore.Mvc;
using Models.DataModels;

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
		
		[HttpPost]
		public IActionResult Reservation([FromBody] dynamic reservation)
		{
			var schedules = new List<Schedule>() {
				new Schedule { Date = new DateOnly(2021, 10, 1), Time = "12:00" },
				new Schedule { Date = new DateOnly(2021, 10, 1), Time = "14:00" },
				new Schedule { Date = new DateOnly(2021, 10, 1), Time = "16:00" },
				new Schedule { Date = new DateOnly(2021, 10, 1), Time = "18:00" },
				new Schedule { Date = new DateOnly(2021, 10, 1), Time = "20:00" }
			};
			return Ok(new { message = schedules });
		}
	}	
}