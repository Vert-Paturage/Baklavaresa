using Application.Reservation.Commands.CreateReservation;
using Domain.Entities;
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

    [HttpGet("Hello")]
    public Task<string> Hello()
    {
        return Task.FromResult("Hello World!");
    } 
    
    [HttpPost("Create")]
    public async Task<IActionResult> CreateReservation(CreateReservation input)
    {
        var command = new CreateReservationCommand(input.FirstName, input.LastName, input.Email);
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
}