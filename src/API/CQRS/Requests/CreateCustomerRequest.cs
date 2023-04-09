using CodeExcercise.Common.Models.DTO;
using MediatR;

namespace CodeExcercise.CQRS.Requests;

/// <summary>
/// CQRS Request - create customer
/// </summary>
public class CreateCustomerRequest : IRequest<Guid>
{
    /// <summary>
    /// Constructor
    /// </summary>
    public CreateCustomerRequest(ClientCustomer inputCustomer)
    {
        ClientCustomer = inputCustomer;
    }

    /// <summary>
    /// Input costumer object to create in database
    /// </summary>
    public ClientCustomer ClientCustomer { get; }
}