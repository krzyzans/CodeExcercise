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
public class GetCustomersHandler : IRequestHandler<GetCustomersRequest, IEnumerable<Customer>>
{
    private readonly ILogger<GetCustomersHandler> logger;
    private readonly IMapService<Customer, DatabaseCustomer> mapService;
    private readonly IRepository<DatabaseCustomer> customerRepository;

    /// <summary>
    /// Constructor
    /// </summary>
    public GetCustomersHandler(
        ILogger<GetCustomersHandler> logger,
        IMapService<Customer, DatabaseCustomer> mapService,
        IRepository<DatabaseCustomer> customerRepository)
    {
        this.logger = logger;
        this.mapService = mapService;
        this.customerRepository = customerRepository;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Customer>> Handle(GetCustomersRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Request for all customers");

        var databaseCustomers = await customerRepository.GetAll(cancellationToken);
        
        return mapService.MapEnumerable(databaseCustomers);
    }
}