using Abstraction.Repositories;
using Abstraction.Services;
using CodeExcercise.Common.Models.Domain;
using CodeExcercise.Common.Models.DTO;
using CodeExcercise.CQRS.Requests;
using MediatR;

namespace CodeExcercise.CQRS.Handlers;

/// <summary>
/// Handler to get customer
/// </summary>
public class CreateCustomerHandler : IRequestHandler<CreateCustomerRequest, Guid>
{
    private readonly ILogger<CreateCustomerHandler> logger;
    private readonly IValidationService<Customer> validationService;
    private readonly IMapService<Customer, DatabaseCustomer> mapperService;
    private readonly IRepository<DatabaseCustomer> customerRepository;

    /// <summary>
    /// Constructor
    /// </summary>
    public CreateCustomerHandler(
        ILogger<CreateCustomerHandler> logger,
        IValidationService<Customer> validationService,
        IMapService<Customer, DatabaseCustomer> mapperService,
        IRepository<DatabaseCustomer> customerRepository)
    {
        this.logger = logger;
        this.validationService = validationService;
        this.mapperService = mapperService;
        this.customerRepository = customerRepository;
    }

    /// <inheritdoc />
    public async Task<Guid> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Create customer: {ClientCustomer}", request.ClientCustomer);

        Customer customer = request.ClientCustomer.MapToCustomer();
        await validationService.ValidateAndThrow(customer, cancellationToken);

        return await customerRepository.Create(mapperService.Map(customer), cancellationToken);
    }
}