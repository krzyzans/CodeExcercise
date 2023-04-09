using Abstraction.Repositories;
using CodeExcercise.Common.Models.DTO;
using CodeExcercise.CQRS.Notification;
using MediatR;

namespace CodeExcercise.CQRS.Handlers;

/// <summary>
/// Handler to get customer
/// </summary>
public class DeleteCustomerHandler : INotificationHandler<DeleteCustomerRequest>
{
    private readonly ILogger<DeleteCustomerHandler> logger;
    private readonly IRepository<DatabaseCustomer> customerRepository;

    /// <summary>
    /// Constructor
    /// </summary>
    public DeleteCustomerHandler(
        ILogger<DeleteCustomerHandler> logger,
        IRepository<DatabaseCustomer> customerRepository)
    {
        this.logger = logger;
        this.customerRepository = customerRepository;
    }

    /// <inheritdoc />
    public async Task Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Delete customer: {CustomerIdent}", request.CustomerIdent);

        await customerRepository.Delete(request.CustomerIdent, cancellationToken);
    }
}