using MedNet.Application.Specifications.Shared;
using MedNet.Domain.Models;
using MedNet.Domain.Repositories;
using MedNet.Domain.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MedNet.WebApi.Controllers.Abstract;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class WithEntityIncludeAttribute<TEntity> : Attribute
{
    public string IncludeString { get; }

    public WithEntityIncludeAttribute(params string[] includeStringPath)
    {
        IncludeString = string.Join('.', includeStringPath);
    }
}

[AttributeUsage(AttributeTargets.Method)]
public class TrackedEntityAttribute<TEntity> : Attribute
{
}

/// <summary>
/// A base class for a custom API controllers with support for entity resolution.
/// </summary>
/// <typeparam name="TEntity">An entity the controller is working with</typeparam>
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public abstract class EntityControllerBase<TEntity> : ManagedControllerBase
    where TEntity : BaseKeyedEntity
{
    private TEntity? _currentEntity = null;

    protected TEntity Entity => _currentEntity ??
                                        throw new InvalidOperationException(
                                            $"{typeof(TEntity).Name} is not loaded. Probably tried to access outside of the controllers action");

    private IReadOnlyRepositoryAsync<TEntity> ReadOnlyRepository => HttpContext.RequestServices.GetRequiredService<IReadOnlyRepositoryAsync<TEntity>>();
    private IWriteRepositoryAsync<TEntity> WriteRepository => HttpContext.RequestServices.GetRequiredService<IWriteRepositoryAsync<TEntity>>();
    
    /// <summary>
    /// Gets called when the entity fetch specification required
    /// </summary>
    /// <param name="id">An id of entity</param>
    /// <returns>Created specification</returns>
    [NonAction]
    protected virtual BaseSpecification<TEntity> GetEntityFetchSpecification(int id)
    {
        return new GetEntityByIdSpecification<TEntity>(id);
    }

    /// <summary>
    /// Gets called when the entity is fetched, so at this stage Entity property is valid and holding a value.
    /// Can be used to interrupt action execution
    /// </summary>
    /// <returns>Action result if action interruption required</returns>
    [NonAction]
    protected virtual Task<IActionResult?> OnEntityFetched()
    {
        return Task.FromResult<IActionResult?>(null);
    }

    [NonAction]
    protected override async Task<bool> OnActionExecutionAsync(ActionExecutingContext context, CancellationToken cancellationToken)
    {
        var previous = await base.OnActionExecutionAsync(context, cancellationToken);
        if (!previous)
        {
            return previous;
        }

        if (!context.RouteData.Values.TryGetValue("id", out var rawId) || 
            rawId is not string idString || 
            !int.TryParse(idString, out var id))
        {
            context.Result = Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: $"An invalid id of {typeof(TEntity).Name} provided: '{rawId}'");
            return false;
        }

        var entityRepository = !IsTrackedEntityRequested() ? ReadOnlyRepository : WriteRepository;
        _currentEntity = await entityRepository.SingleOrDefaultAsync(CookSpecificationIncludes(GetEntityFetchSpecification(id)), cancellationToken);
        if (_currentEntity == null)
        {
            context.Result = Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: $"A {typeof(TEntity).Name} with id '{id}' was not found");
            return false;
        }

        var actionResult = await OnEntityFetched();
        if (actionResult != null)
        {
            context.Result = actionResult;
            return false;
        }
        return true;
    }

    [NonAction]
    private bool IsTrackedEntityRequested()
    {
        var endpoint = HttpContext.GetEndpoint();
        if (endpoint == null)
            return false;

        // Look for the attribute (non-generic search, because of runtime generic type issues)
        var trackedAttr = endpoint.Metadata
            .FirstOrDefault(attr =>
                attr is TrackedEntityAttribute<TEntity>) as TrackedEntityAttribute<TEntity>;

        return trackedAttr != null;
    }
    
    [NonAction]
    private BaseSpecification<TEntity> CookSpecificationIncludes(BaseSpecification<TEntity> cookingSpecification)
    {
        var endpoint = ControllerContext.HttpContext.GetEndpoint();
        if (endpoint != null)
        {
            var includeAttributes = endpoint.Metadata.Where(m => m is WithEntityIncludeAttribute<TEntity>)
                .Cast<WithEntityIncludeAttribute<TEntity>>();

            foreach (var includeAttr in includeAttributes)
            {
                cookingSpecification.AddInclude(includeAttr.IncludeString);
            }
        }

        return cookingSpecification;
    }
}

/// <summary>
/// A base class for a custom API controllers with support for entity resolution.
/// </summary>
/// <typeparam name="TParentEntity">A parent entity the controller entity is depending on</typeparam>
/// <typeparam name="TEntity">An entity the controller is working with</typeparam>
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public abstract class EntityControllerBase<TParentEntity, TEntity> : ManagedControllerBase
    where TParentEntity : BaseKeyedEntity
    where TEntity : BaseKeyedEntity
{
    private TParentEntity? _parentEntity = null;
    private TEntity? _childEntity = null;

    protected TParentEntity ParentEntity => _parentEntity ??
                                            throw new InvalidOperationException(
                                                $"{typeof(TParentEntity).Name} is not loaded. Probably tried to access outside of the controllers action");
    
    protected TEntity Entity => _childEntity ??
                                     throw new InvalidOperationException(
                                         $"{typeof(TEntity).Name} is not loaded. Probably tried to access outside of the controllers action");

    private IReadOnlyRepositoryAsync<TParentEntity> ReadOnlyParentRepository => HttpContext.RequestServices.GetRequiredService<IReadOnlyRepositoryAsync<TParentEntity>>();
    private IWriteRepositoryAsync<TParentEntity> WriteParentRepository => HttpContext.RequestServices.GetRequiredService<IWriteRepositoryAsync<TParentEntity>>();
    
    private IReadOnlyRepositoryAsync<TEntity> ReadOnlyRepository => HttpContext.RequestServices.GetRequiredService<IReadOnlyRepositoryAsync<TEntity>>();
    private IWriteRepositoryAsync<TEntity> WriteRepository => HttpContext.RequestServices.GetRequiredService<IWriteRepositoryAsync<TEntity>>();
    
    /// <summary>
    /// Gets called when the entities are fetched, so at this stage ParentEntity and Entity properties are valid and holding a value.
    /// Can be used to interrupt action execution
    /// </summary>
    /// <returns>Action result if action interruption required</returns>
    [NonAction]
    protected virtual Task<IActionResult?> OnEntitiesFetched()
    {
        return Task.FromResult<IActionResult?>(null);
    }

    [NonAction]
    protected override async Task<bool> OnActionExecutionAsync(ActionExecutingContext context, CancellationToken cancellationToken)
    {
        var previous = await base.OnActionExecutionAsync(context, cancellationToken);
        if (!previous)
        {
            return previous;
        }

        if (!context.RouteData.Values.TryGetValue("parentid", out var rawParentId) || 
            rawParentId is not string parentIdString || 
            !int.TryParse(parentIdString, out var parentId))
        {
            context.Result = Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: $"An invalid id of {typeof(TParentEntity).Name} provided: '{rawParentId}'");
            return false;
        }
        
        if (!context.RouteData.Values.TryGetValue("id", out var rawChildId) || 
            rawChildId is not string idChildString || 
            !int.TryParse(idChildString, out var childId))
        {
            context.Result = Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: $"An invalid id of {typeof(TEntity).Name} provided: '{rawChildId}'");
            return false;
        }

        var parentRepository = !IsTrackedEntityRequested<TParentEntity>() ? ReadOnlyParentRepository : WriteParentRepository;
        _parentEntity = await parentRepository.SingleOrDefaultAsync(CookSpecificationIncludes(GetParentEntityFetchSpecification(parentId)), cancellationToken);
        if (_parentEntity == null)
        {
            context.Result = Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: $"A {typeof(TParentEntity).Name} with id '{parentId}' was not found");
            return false;
        }

        var childRepository = !IsTrackedEntityRequested<TEntity>() ? ReadOnlyRepository : WriteRepository;
        _childEntity = await childRepository.SingleOrDefaultAsync(CookSpecificationIncludes(GetEntityFetchSpecification(childId)), cancellationToken);
        if (_childEntity == null)
        {
            context.Result = Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: $"A {typeof(TEntity).Name} with id '{childId}' was not found");
            return false;
        }

        var actionResult = await OnEntitiesFetched();
        if (actionResult != null)
        {
            context.Result = actionResult;
            return false;
        }

        return true;
    }

    [NonAction]
    protected virtual BaseSpecification<TParentEntity> GetParentEntityFetchSpecification(int id)
    {
        return new GetEntityByIdSpecification<TParentEntity>(id);
    }
    
    [NonAction]
    protected virtual BaseSpecification<TEntity> GetEntityFetchSpecification(int id)
    {
        return new GetEntityByIdSpecification<TEntity>(id);
    }
    
    [NonAction]
    private bool IsTrackedEntityRequested<TCheckingEntity>()
    {
        var endpoint = HttpContext.GetEndpoint();
        if (endpoint == null)
            return false;

        // Look for the attribute (non-generic search, because of runtime generic type issues)
        var trackedAttr = endpoint.Metadata
            .FirstOrDefault(attr =>
                attr is TrackedEntityAttribute<TCheckingEntity>) as TrackedEntityAttribute<TCheckingEntity>;

        return trackedAttr != null;
    }
    
    [NonAction]
    private BaseSpecification<TApplyingEntity> CookSpecificationIncludes<TApplyingEntity>(BaseSpecification<TApplyingEntity> cookingSpecification)
        where TApplyingEntity : BaseEntity
    {
        var endpoint = ControllerContext.HttpContext.GetEndpoint();
        if (endpoint != null)
        {
            var includeAttributes = endpoint.Metadata.Where(m => m is WithEntityIncludeAttribute<TApplyingEntity>)
                .Cast<WithEntityIncludeAttribute<TApplyingEntity>>();

            foreach (var includeAttr in includeAttributes)
            {
                cookingSpecification.AddInclude(includeAttr.IncludeString);
            }
        }

        return cookingSpecification;
    }
}
