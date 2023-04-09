using CodeExcercise.Common.Models.Domain;
using MediatR;

namespace CodeExcercise.CQRS.Requests;

/// <summary>
/// CQRS Request - get customer
/// </summary>
public class GetCustomersRequest : IRequest<IEnumerable<Customer>>
{

}