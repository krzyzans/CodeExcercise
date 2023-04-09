using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Abstraction.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll(CancellationToken cancelationToken);
        Task Delete(Guid ident, CancellationToken cancelationToken);
        Task<Guid> Create(T entity, CancellationToken cancelationToken);
    }
}