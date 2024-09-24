using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;

using ECommerce.Shared.Common.AggregatesModel.Auditing;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Services;
using ECommerce.Shared.Common.Mediator;

namespace ECommerce.Shared.Common.Data.Interceptors;

public class SaveChangesInterceptor : ISaveChangesInterceptor
{
    public void SaveChangesFailed(DbContextErrorEventData eventData)
    {

    }

    public Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        return result;
    }

    public async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context != null)
        {
            var publisher = eventData.Context.GetService<IEventPublisher>();

            await publisher.DispatchDomainEventsAfterSaveAsync(eventData.Context);
        }

        return await ValueTask.FromResult(result);
    }

    public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        return result;
    }

    public async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context != null)
        {
            var publisher = eventData.Context.GetService<IEventPublisher>();

            publisher.ValidateInvalidDomainEvents(eventData.Context);

            // Dispatch Domain Events collection.
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB. This makes
            // a single transaction including side effects from the domain event
            // handlers that are using the same DbContext with Scope lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB. This makes
            // multiple transactions. You will need to handle eventual consistency and
            // compensatory actions in case of failures.
            await publisher.DispatchDomainEventsBeforeSaveAsync(eventData.Context);

            await ProcessingData(eventData.Context, cancellationToken);
        }

        return result;
    }

    private async Task ProcessingData(DbContext dbContext, CancellationToken cancellationToken = default)
    {
        dbContext.ChangeTracker.DetectChanges();
        var identityService = dbContext.GetService<IIdentityService>();

        var newEntities = dbContext.ChangeTracker.Entries()
            .Where(t => t.State == EntityState.Added && t.Entity is ICreationAuditing)
            .Select(t => t.Entity as ICreationAuditing);

        foreach (ICreationAuditing? entity in newEntities)
        {
            // TODO: using after identity service working
            // if (entity?.CreatedBy == Guid.Empty)
            entity!.AuditCreation(identityService.UserId);
        }

        var editingEntries = dbContext.ChangeTracker.Entries()
            .Where(t => t.State == EntityState.Modified && t.Entity is IUpdateAuditing)
            .ToList();

        foreach (var entry in editingEntries)
        {
            if (PatchEntityData(entry))
            {
                // TODO: using after identity service working
                if (entry.Entity is IUpdateAuditing editableEntity)
                {
                    // if (editableEntity.UpdatedBy == Guid.Empty)
                    editableEntity.AuditUpdate(identityService.UserId);
                }
            }
        }

        var softDeletingEntries = dbContext.ChangeTracker.Entries()
            .Where(t => t.State == EntityState.Deleted && t.Entity is IDeletionAuditing);

        foreach (var entry in softDeletingEntries)
        {
            entry.State = EntityState.Unchanged;

            await AuditSoftDeletingEntity(entry, identityService);
        }
    }

    #region Auditing

    private async Task AuditSoftDeletingEntity(EntityEntry entry, IIdentityService identityService, CancellationToken cancellationToken = default)
    {
        if (entry.Entity is IDeletionAuditing deletingEntity)
        {
            deletingEntity.AuditDeletion(identityService.UserId);

            await AuditCascadeSoftDeletingEntities(entry, cancellationToken);
        }
    }

    private async Task AuditCascadeSoftDeletingEntities(EntityEntry entry, CancellationToken cancellationToken = default)
    {
        // var tasks = entry.Collections.Select(async collectionEntry =>
        // {
        //     await collectionEntry.LoadAsync(cancellationToken);

        //     if (collectionEntry.EntityEntry is IDeletionAuditing deletingEntity)
        //     {
        //         collectionEntry.EntityEntry.State = EntityState.Unchanged;

        //         await AuditSoftDeletingEntity(collectionEntry.EntityEntry);
        //     }

        //     return true;
        // });

        // await Task.WhenAll(tasks);

        await Task.CompletedTask;
    }

    #endregion

    private static bool PatchEntityData(EntityEntry entry)
    {
        PropertyValues originalValues = entry.OriginalValues;
        PropertyValues currentValues = entry.CurrentValues;

        IEnumerable<string> patchingPropertiesName = currentValues.Properties
            .Where(p => !p.IsKey())
            .Select(p => p.Name);

        foreach (var propertyName in patchingPropertiesName)
        {
            // TODO: Workaround solution for checking json array property
            // if this property inherit the abstract interface like ICollection or IReadOnlyCollection
            if (entry.Property(propertyName).Metadata.ClrType.IsAbstract)
            {
                continue;
            }

            entry.Property(propertyName).IsModified = !Equals(originalValues[propertyName], currentValues[propertyName]);
        }

        return entry.Properties.Any(x => x.IsModified);
    }
}
