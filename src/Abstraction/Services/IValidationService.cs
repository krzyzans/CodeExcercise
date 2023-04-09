using System.Threading;
using System.Threading.Tasks;

namespace Abstraction.Services
{
    public interface IValidationService<T>
    {
        Task ValidateAndThrow(T validate, CancellationToken cancelationToken);
    }
}