using Application.Reservation.Queries.GetAllTables;
using Application.Table.Commands.CreateTable;
using Application.Table.Commands.DeleteTable;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Inputs.Table;
using System;

namespace Web.Controllers;

[ApiController]
[Route("/[controller]")]
public class TableController: ControllerBase
{
   private readonly IMediator _mediator;
   
   public TableController(IMediator mediator)
   {
       _mediator = mediator;
   }
   
   [HttpPost("Create")]
   public async Task<IActionResult> CreateTable(CreateTable input)
   {
       Console.WriteLine("TableController.CreateTable");
       var command = new CreateTableCommand(input.Capacity);
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

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllTables()
    {
        var query = new GetAllTablesQuery();
        try
        {
            var tables = await _mediator.Send(query);
            return Ok(tables);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTable(int id)
    {
        Console.WriteLine("TableController.DeleteTable");
        var command = new DeleteTableCommand(id);
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