
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace OrderingInfrastructure.Data.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{

    private static void UpdateEntities(DbContext context)
    {
        var entries = context.ChangeTracker
            .Entries<IEntity>();

        var utcNow = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = utcNow;
            }

            // Populate also the UpdatedAt on both Added and Modified states
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
            {
                entry.Entity.UpdatedAt = utcNow;
            }
        }
    }

    public override InterceptionResult<int> SavingChanges(
         DbContextEventData eventData,
         InterceptionResult<int> result)
    {
        var context = eventData.Context;

        if (context == null)
            return base.SavingChanges(eventData, result);

        UpdateEntities(context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;

        if (context == null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);

        UpdateEntities(context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry)
    {
        // Check owned reference navigations
        if (entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added ||
             r.TargetEntry.State == EntityState.Modified ||
             r.TargetEntry.State == EntityState.Deleted)))
        {
            return true;
        }

        // Check owned collection navigations (iterate current items and inspect their entries)
        if (entry.Context != null)
        {
            foreach (var collection in entry.Collections)
            {
                if (collection.CurrentValue is not System.Collections.IEnumerable current)
                    continue;

                foreach (var ownedEntity in current)
                {
                    var ownedEntry = entry.Context.Entry(ownedEntity);

                    if (ownedEntry != null &&
                        ownedEntry.Metadata.IsOwned() &&
                        (ownedEntry.State == EntityState.Added ||
                         ownedEntry.State == EntityState.Modified ||
                         ownedEntry.State == EntityState.Deleted))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
