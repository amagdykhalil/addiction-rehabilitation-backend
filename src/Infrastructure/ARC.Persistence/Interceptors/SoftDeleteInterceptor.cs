using ARC.Application.Abstractions.UserContext;
using ARC.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ARC.Persistence.Data.Interceptors
{
    public sealed class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        private readonly IUserContext _userContext;
        public SoftDeleteInterceptor(IUserContext userContext)
        {
            _userContext = userContext;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null)
            {
                return base.SavingChangesAsync(
                    eventData, result, cancellationToken);
            }

            IEnumerable<EntityEntry<ISoftDeleteable>> entries =
                eventData
                    .Context
                    .ChangeTracker
                    .Entries<ISoftDeleteable>()
                    .Where(e => e.State == EntityState.Deleted);

            foreach (EntityEntry<ISoftDeleteable> softDeletable in entries)
            {
                softDeletable.State = EntityState.Modified;
                softDeletable.Entity.DeletedAt = DateTime.UtcNow;
                softDeletable.Entity.DeletedBy = _userContext.UserId;
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}



