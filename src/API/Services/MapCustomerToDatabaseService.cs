using Abstraction.Services;
using CodeExcercise.Common.Models.Domain;
using CodeExcercise.Common.Models.DTO;

namespace CodeExcercise.Services;

/// <summary>
/// Mapper customer to databaseCustomer both direction 
/// </summary>
public class MapCustomerToDatabaseService : IMapService<Customer, DatabaseCustomer>
{
    /// <inheritdoc />
    public DatabaseCustomer Map(Customer input)
    {
        return new DatabaseCustomer(
            input.Ident,
            input.Firstname,
            input.Surname);
    }

    /// <inheritdoc />
    public Customer Map(DatabaseCustomer input)
    {
        return new Customer(
            input.Ident,
            input.Firstname,
            input.Surname);
    }

    /// <inheritdoc />
    public IEnumerable<DatabaseCustomer> MapEnumerable(IEnumerable<Customer> customerEnumerable)
    {
        IList<DatabaseCustomer> databaseCustomers = new List<DatabaseCustomer>();

        foreach (var customer in customerEnumerable) 
        {
            databaseCustomers.Add(Map(customer));
        }

        return databaseCustomers;
    }

    /// <inheritdoc />
    public IEnumerable<Customer> MapEnumerable(IEnumerable<DatabaseCustomer> databaseCustomerEnumerable)
    {
        IList<Customer> customers = new List<Customer>();

        foreach (var databaseCustomer in databaseCustomerEnumerable)
        {
            customers.Add(Map(databaseCustomer));
        }

        return customers;
    }
}