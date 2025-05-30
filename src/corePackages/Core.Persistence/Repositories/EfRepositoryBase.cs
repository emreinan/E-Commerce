using Core.Persistence.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Persistence.Repositories;

public class EfRepositoryBase<TEntity, TEntityId, TContext>(TContext context)
    : IAsyncRepository<TEntity, TEntityId>,
        IRepository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>, new()
    where TContext : DbContext
{
    protected readonly TContext Context = context;

    public IQueryable<TEntity> Query()
    {
        return Context.Set<TEntity>();
    }

    protected virtual void EditEntityPropertiesToAdd(TEntity entity)
    {
        entity.CreatedDate = DateTime.UtcNow;
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        EditEntityPropertiesToAdd(entity);
        await Context.AddAsync(entity, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<ICollection<TEntity>> AddRangeAsync(
        ICollection<TEntity> entities,
        CancellationToken cancellationToken = default
    )
    {
        foreach (TEntity entity in entities)
            EditEntityPropertiesToAdd(entity);
        await Context.AddRangeAsync(entities, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        return entities;
    }

    protected virtual void EditEntityPropertiesToUpdate(TEntity entity)
    {
        entity.UpdatedDate = DateTime.UtcNow;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        EditEntityPropertiesToUpdate(entity);
        Context.Update(entity);
        await Context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<ICollection<TEntity>> UpdateRangeAsync(
        ICollection<TEntity> entities,
        CancellationToken cancellationToken = default
    )
    {
        foreach (TEntity entity in entities)
            EditEntityPropertiesToUpdate(entity);
        Context.UpdateRange(entities);
        await Context.SaveChangesAsync(cancellationToken);
        return entities;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity, bool permanent = false, CancellationToken cancellationToken = default)
    {
        await SetEntityAsDeleted(entity, permanent, isAsync: true, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<ICollection<TEntity>> DeleteRangeAsync(
        ICollection<TEntity> entities,
        bool permanent = false,
        CancellationToken cancellationToken = default
    )
    {
        await SetEntityAsDeleted(entities, permanent, isAsync: true, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        return entities;
    }

    public async Task<ICollection<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include != null)
            queryable = include(queryable);
        if (withDeleted)
            queryable = queryable.IgnoreQueryFilters();
        if (predicate != null)
            queryable = queryable.Where(predicate);
        if (orderBy != null)
            return await orderBy(queryable).ToListAsync(cancellationToken: cancellationToken);
        return await queryable.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include != null)
            queryable = include(queryable);
        if (withDeleted)
            queryable = queryable.IgnoreQueryFilters();
        return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
    }


    public async Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
        bool withDeleted = false,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<TEntity> queryable = Query();
        if (withDeleted)
            queryable = queryable.IgnoreQueryFilters();
        if (predicate != null)
            queryable = queryable.Where(predicate);
        return await queryable.AnyAsync(cancellationToken);
    }

    public TEntity Add(TEntity entity)
    {
        EditEntityPropertiesToAdd(entity);
        Context.Add(entity);
        Context.SaveChanges();
        return entity;
    }

    public ICollection<TEntity> AddRange(ICollection<TEntity> entities)
    {
        foreach (TEntity entity in entities)
            EditEntityPropertiesToAdd(entity);
        Context.AddRange(entities);
        Context.SaveChanges();
        return entities;
    }

    public TEntity Update(TEntity entity)
    {
        EditEntityPropertiesToAdd(entity);
        Context.Update(entity);
        Context.SaveChanges();
        return entity;
    }

    public ICollection<TEntity> UpdateRange(ICollection<TEntity> entities)
    {
        foreach (TEntity entity in entities)
            EditEntityPropertiesToAdd(entity);
        Context.UpdateRange(entities);
        Context.SaveChanges();
        return entities;
    }

    public TEntity Delete(TEntity entity, bool permanent = false)
    {
        SetEntityAsDeleted(entity, permanent, isAsync: false).Wait();
        Context.SaveChanges();
        return entity;
    }

    public ICollection<TEntity> DeleteRange(ICollection<TEntity> entities, bool permanent = false)
    {
        SetEntityAsDeleted(entities, permanent, isAsync: false).Wait();
        Context.SaveChanges();
        return entities;
    }

    public TEntity? Get(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true
    )
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include != null)
            queryable = include(queryable);
        if (withDeleted)
            queryable = queryable.IgnoreQueryFilters();
        return queryable.FirstOrDefault(predicate);
    }

    public ICollection<TEntity> GetList(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true
    )
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include != null)
            queryable = include(queryable);
        if (withDeleted)
            queryable = queryable.IgnoreQueryFilters();
        if (predicate != null)
            queryable = queryable.Where(predicate);
        if (orderBy != null)
            return orderBy(queryable).ToList();
        return queryable.ToList();
    }

    public bool Any(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
        bool withDeleted = false
    )
    {
        IQueryable<TEntity> queryable = Query();
        if (withDeleted)
            queryable = queryable.IgnoreQueryFilters();
        if (predicate != null)
            queryable = queryable.Where(predicate);
        return queryable.Any();
    }

    protected async Task SetEntityAsDeleted(
        TEntity entity,
        bool permanent,
        bool isAsync = true,
        CancellationToken cancellationToken = default
    )
    {
        if (!permanent)
        {
            CheckHasEntityHaveOneToOneRelation(entity);
            if (isAsync)
                await SetEntityAsSoftDeleted(entity, isAsync, cancellationToken);
            else
                SetEntityAsSoftDeleted(entity, isAsync).Wait(cancellationToken);
        }
        else
            Context.Remove(entity);
    }

    protected async Task SetEntityAsDeleted(
        IEnumerable<TEntity> entities,
        bool permanent,
        bool isAsync = true,
        CancellationToken cancellationToken = default
    )
    {
        foreach (TEntity entity in entities)
            await SetEntityAsDeleted(entity, permanent, isAsync, cancellationToken);
    }

    protected IQueryable<object>? GetRelationLoaderQuery(IQueryable query, Type navigationPropertyType)
    {
        Type queryProviderType = query.Provider.GetType();
        MethodInfo createQueryMethod =
            queryProviderType
                .GetMethods()
                .First(m => m is { Name: nameof(query.Provider.CreateQuery), IsGenericMethod: true })
                ?.MakeGenericMethod(navigationPropertyType)
            ?? throw new InvalidOperationException("CreateQuery<TElement> method is not found in IQueryProvider.");
        var queryProviderQuery = (IQueryable<object>)createQueryMethod.Invoke(query.Provider, parameters: [query.Expression])!;
        return queryProviderQuery.Where(x => !((TEntity)x).DeletedDate.HasValue);
    }

    protected void CheckHasEntityHaveOneToOneRelation(TEntity entity)
    {
        IEnumerable<IForeignKey> foreignKeys = Context.Entry(entity).Metadata.GetForeignKeys();
        IForeignKey? oneToOneForeignKey = foreignKeys.FirstOrDefault(fk =>
            fk.IsUnique && fk.PrincipalKey.Properties.All(pk => Context.Entry(entity).Property(pk.Name).Metadata.IsPrimaryKey())
        );

        if (oneToOneForeignKey != null)
        {
            string relatedEntity = oneToOneForeignKey.PrincipalEntityType.ClrType.Name;
            IReadOnlyList<IProperty> primaryKeyProperties = Context.Entry(entity).Metadata.FindPrimaryKey()!.Properties;
            string primaryKeyNames = string.Join(", ", primaryKeyProperties.Select(prop => prop.Name));
            throw new InvalidOperationException(
                $"Entity {entity.GetType().Name} has a one-to-one relationship with {relatedEntity} via the primary key ({primaryKeyNames}). Soft Delete causes problems if you try to create an entry again with the same foreign key."
            );
        }
    }

    protected virtual bool IsSoftDeleted(object entity)
    {
        var prop = entity.GetType().GetProperty("DeletedDate");
        if (prop == null) return false;
        var value = prop.GetValue(entity);
        return value != null;
    }

    protected virtual void EditEntityPropertiesToDelete(object entity)
    {
        var prop = entity.GetType().GetProperty("DeletedDate");
        if (prop != null && prop.CanWrite)
            prop.SetValue(entity, DateTime.UtcNow);
    }

    protected virtual void EditRelationEntityPropertiesToCascadeSoftDelete(object entity)
    {
        var prop = entity.GetType().GetProperty("DeletedDate");
        if (prop != null && prop.CanWrite)
            prop.SetValue(entity, DateTime.UtcNow);
    }


    private async Task SetEntityAsSoftDeleted(
     object entity,
     bool isAsync = true,
     CancellationToken cancellationToken = default,
     bool isRoot = true
 )
    {
        ArgumentNullException.ThrowIfNull(entity);

        if (IsSoftDeleted(entity))
            return;

        if (isRoot)
            EditEntityPropertiesToDelete(entity);
        else
            EditRelationEntityPropertiesToCascadeSoftDelete(entity);

        var entry = Context.Entry(entity);
        var navigations = entry.Metadata.GetNavigations()
            .Where(x => x is { IsOnDependent: false, ForeignKey.DeleteBehavior: DeleteBehavior.ClientCascade or DeleteBehavior.Cascade })
            .ToList();

        foreach (var navigation in navigations)
        {
            if (navigation.TargetEntityType.IsOwned() || navigation.PropertyInfo == null)
                continue;

            object? navValue = navigation.PropertyInfo.GetValue(entity);

            if (navigation.IsCollection)
            {
                if (navValue == null)
                {
                    var elementType = navigation.TargetEntityType.ClrType;
                    IQueryable query = entry.Collection(navigation.PropertyInfo.Name).Query();

                    if (isAsync)
                    {
                        var toListAsyncMethod = ReflectionHelpers.GetGenericMethod(
                            typeof(EntityFrameworkQueryableExtensions), "ToListAsync", 2, elementType);

                        var task = (Task?)toListAsyncMethod.Invoke(null, [query, cancellationToken]);
                        if (task == null)
                            continue;

                        await task.ConfigureAwait(false);
                        navValue = ReflectionHelpers.GetTaskResult(task);
                    }
                    else
                    {
                        var toListMethod = ReflectionHelpers.GetGenericMethod(typeof(Enumerable), "ToList", 1, elementType);
                        navValue = toListMethod.Invoke(null, [query]);
                    }

                    if (navValue == null)
                        continue;
                }

                foreach (var navValueItem in (IEnumerable)navValue)
                {
                    if (navValueItem != null)
                        await SetEntityAsSoftDeleted(navValueItem, isAsync, cancellationToken, isRoot: false);
                }
            }
            else // Reference navigation
            {
                if (navValue == null)
                {
                    var elementType = navigation.TargetEntityType.ClrType;
                    IQueryable query = entry.Reference(navigation.PropertyInfo.Name).Query();

                    if (isAsync)
                    {
                        var firstOrDefaultAsyncMethod = ReflectionHelpers.GetGenericMethod(
                            typeof(EntityFrameworkQueryableExtensions), "FirstOrDefaultAsync", 2, elementType);

                        var task = (Task?)firstOrDefaultAsyncMethod.Invoke(null, [query, cancellationToken]);
                        if (task == null)
                            continue;

                        await task.ConfigureAwait(false);
                        navValue = ReflectionHelpers.GetTaskResult(task);
                    }
                    else
                    {
                        var firstOrDefaultMethod = ReflectionHelpers.GetGenericMethod(typeof(Queryable), "FirstOrDefault", 1, elementType);
                        navValue = firstOrDefaultMethod.Invoke(null, [query]);
                    }

                    if (navValue == null)
                        continue;
                }

                await SetEntityAsSoftDeleted(navValue, isAsync, cancellationToken, isRoot: false);
            }
        }

        Context.Update(entity);
    }

    public static class ReflectionHelpers
    {
        public static MethodInfo GetGenericMethod(Type type, string name, int paramCount, Type genericArg)
        {
            var method = type.GetMethods()
                .FirstOrDefault(m => m.Name == name && m.GetParameters().Length == paramCount);
            return method == null
                ? throw new InvalidOperationException($"{name} method not found on {type.Name}.")
                : method.MakeGenericMethod(genericArg);
        }

        public static object? GetTaskResult(Task task)
        {
            var resultProperty = task.GetType().GetProperty("Result");
            return resultProperty?.GetValue(task);
        }
    }


}
