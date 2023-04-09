using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace CodeExcercise.Database.Context.MemoryDatabase;

/// <inheritdoc />
public class GuidValueGenerator : ValueGenerator<Guid>
{
    private readonly object syncObject = new object();

    /// <inheritdoc />
    public override bool GeneratesTemporaryValues => false;

    /// <inheritdoc />
    public override Guid Next(EntityEntry entry)
    {
        lock (syncObject)
        {
            return Guid.NewGuid();
        }
    }
}