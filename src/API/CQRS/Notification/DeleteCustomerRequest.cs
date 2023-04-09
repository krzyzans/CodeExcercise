using MediatR;

namespace CodeExcercise.CQRS.Notification;

/// <summary>
/// CQRS Notification - delete customer
/// </summary>
public class DeleteCustomerRequest : INotification
{
    /// <summary>
    /// Constructor
    /// </summary>
    public DeleteCustomerRequest(Guid customerIdent)
    {
        CustomerIdent = customerIdent;
    }

    /// <summary>
    /// Customer Ident to delete
    /// </summary>
    public Guid CustomerIdent { get; }
}