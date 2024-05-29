using Application.Reservation.Commands.CreateReservation;
using Application.Reservation.Queries.GetReservationById;
using Domain.Repositories;
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
            input.LastName, input.Email, input.Date, input.NumberOfPeople, input.Tables);
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
    
    [HttpGet("AvailableSlots/{day}")]
    public async Task<IActionResult> GetAvailableSlots(GetReservationByDay input)
    {
        /*var query = new GetAvailableSlotsQuery(input);
        try
        {
            var timeSlots = await _mediator.Send(query);
            return Ok(timeSlots);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }*/
        return Ok();
    }
    [HttpGet("AvailableSlots/{month}")]
    public async Task<IActionResult> GetAvailableSlots(GetReservationByMonth input)
    {
        /*var query = new GetAvailableSlotsQuery(input);
        try
        {
            var timeSlots = await _mediator.Send(query);
            return Ok(timeSlots);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }*/
        return Ok();
    }
}