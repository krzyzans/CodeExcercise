using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace CodeExcercise.Database.Context.MemoryDatabase;

/// <inheritdoc />
public class OrderIdValueGenerator : ValueGenerator<long>
{
    private int current;

    /// <inheritdoc />
    public override bool GeneratesTemporaryValues => false;

    /// <inheritdoc />
    public override long Next(EntityEntry entry)
        => Interlocked.Increment(ref current);
}