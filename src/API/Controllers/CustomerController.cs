using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CodeExcercise.Common.Helpers;
using CodeExcercise.Common.Models;
using CodeExcercise.Common.Models.Domain;
using CodeExcercise.Common.Models.DTO;
using CodeExcercise.CQRS.Notification;
using CodeExcercise.CQRS.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeExcercise.Controllers;

/// <summary>
/// Customer Controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor
    /// </summary>
    public CustomerController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    /// <summary>
    /// HttpGet
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Customer>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<ActionResult<IEnumerable<Customer>>> Get()
    { 
        var customers = await mediator.Send(new GetCustomersRequest(), CancelationTokenHelper.CreateCancelationToken());

        return StatusCode(StatusCodes.Status200OK, customers);
    }

    // POST: api/controller
    /// <summary>
    /// Creates new customer
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorResponse))]
    public async Task<ActionResult<Guid>> Post([FromBody] ClientCustomer value)
    {
        var customerIdent = await mediator.Send(new CreateCustomerRequest(value), CancelationTokenHelper.CreateCancelationToken());

        return StatusCode(StatusCodes.Status201Created, customerIdent);
    }

    // DELETE: api/controller/5
    /// <summary>
    /// Delete customer controller method
    /// </summary>
    [HttpDelete("{ident}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult Delete([Required][FromRoute] Guid ident)
    {
        mediator.Publish(new DeleteCustomerRequest(ident));

        return StatusCode(StatusCodes.Status204NoContent);
    }
}