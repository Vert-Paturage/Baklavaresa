using Application.Table.Commands.CreateTable;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Inputs.Table;

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
}