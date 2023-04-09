using System.Collections.Generic;

namespace Abstraction.Services;

public interface IMapService<T1, T2> : IMapServiceBase
{
    public IEnumerable<T2> MapEnumerable(IEnumerable<T1> inputEnumerable);

    public T2 Map(T1 inputEnumerable);

    public IEnumerable<T1> MapEnumerable(IEnumerable<T2> inputEnumerable);

    public T1 Map(T2 inputEnumerable);
}