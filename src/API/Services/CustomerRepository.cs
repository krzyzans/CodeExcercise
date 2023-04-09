using Abstraction.Repositories;
using CodeExcercise.Common.Configuration;
using CodeExcercise.Common.Exceptions;
using CodeExcercise.Common.Models.DTO;
using CodeExcercise.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CodeExcercise.Services;

/// <summary>
/// Repository for customer repository
/// </summary>
public class CustomerRepository : IRepository<DatabaseCustomer>
{
    private readonly TopLimitsConfiguration topLimitConfiguration;
    private readonly CustomerContext context;

    /// <summary>
    /// Constructor
    /// </summary>
    public CustomerRepository(
        IOptions<TopLimitsConfiguration> topLimitConfiguration,
        CustomerContext context)
    {
        this.topLimitConfiguration = topLimitConfiguration.Value;
        this.context = context;
    }
    
    /// <inheritdoc />
    public async Task<IEnumerable<DatabaseCustomer>> GetAll(CancellationToken cancelationToken)
    {
        return await context.Customers.Take(topLimitConfiguration.DefaultTopValue).ToListAsync();
    }

    /// <inheritdoc />
    public async Task Delete(Guid ident, CancellationToken cancellationToken)
    {
        var databaseCustomer = await context.Customers.FirstOrDefaultAsync(cancellationToken);
        if (databaseCustomer == null)
        {
            throw new CustomerNotExistsException(String.Format("Customer with ident: {ident} not exists in database", ident));
        }

        context.Customers.Remove(databaseCustomer);
        await context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Guid> Create(DatabaseCustomer customer, CancellationToken cancelationToken)
    {
        var databaseCustomer = await context.Customers.AddAsync(customer, cancelationToken);
        await context.SaveChangesAsync(cancelationToken);

        return await Task.FromResult(databaseCustomer.Entity.Ident);
    }
}