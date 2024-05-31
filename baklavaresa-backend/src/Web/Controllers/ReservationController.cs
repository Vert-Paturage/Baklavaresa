using Application.Reservation.Commands.CreateReservation;
using Application.Reservation.Queries.GetAllReservations;
using Application.Reservation.Queries.GetAvailableSlots;
using Application.Reservation.Queries.GetReservationById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Inputs.Reservation;

namespace Web.Controllers;

[ApiController]
[Route("/[controller]")]
public class ReservationController: ControllerBase
{
    private readonly IMediator _mediator;

    public ReservationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateReservation(CreateReservation input)
    {
        var command = new CreateReservationCommand(input.FirstName,
            input.LastName, input.Email, input.Date, input.NumberOfPeople);
        try
        {
            await _mediator.Send(command);
            return Ok(); 
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetReservation(int id)
    {
        var query = new GetReservationByIdQuery(id);
        try
        {
            var reservation = await _mediator.Send(query);
            return Ok(reservation);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("GetAvailableSlots")]
    public async Task<IActionResult> GetAvailableSlots(Inputs.Reservation.GetAvailableSlots input)
    {
        System.Diagnostics.Debug.WriteLine(input);
        if (input.NumberOfPeople <= 0)
        {
            return BadRequest("Number of people must be greater than 0");
        }
        var query = new GetAvailableSlotsQuery(input.NumberOfPeople, input.Date);
        try
        {
            var availableSlots = await _mediator.Send(query);
            if (!availableSlots.Any())
            {
                return NotFound();
            }
            return Ok(availableSlots);
        }
        catch (Exception e)
        {
            return BadRequest(e.StackTrace);
        }
    } 

    [HttpGet("GetAllReservations")]
    public async Task<IActionResult> GetAllReservations(DateTime input)
    {
        System.Diagnostics.Debug.WriteLine(input);
        var query = new GetAllReservationsQuery(input);
        try
        {
            var availableSlots = await _mediator.Send(query);
            return Ok(availableSlots);
        }
        catch (Exception e)
        {
            return BadRequest(e.StackTrace);
        }
    } 
}