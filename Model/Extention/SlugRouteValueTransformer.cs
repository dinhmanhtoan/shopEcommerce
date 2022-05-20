using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace Model.Extention;

public class SlugRouteValueTransformer : DynamicRouteValueTransformer
{
    private readonly IRepository<Entity> _entityRepository;

    public SlugRouteValueTransformer(IRepository<Entity> entityRespository)
    {
        _entityRepository = entityRespository;
    }
    public override async ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
    {
        var requestPath = httpContext.Request.Path.Value;

        if (!string.IsNullOrEmpty(requestPath) && requestPath[0] == '/')
        {
            requestPath = requestPath.Substring(1);

        }
        var entity = await _entityRepository.Query().Include(x => x.EntityType).FirstOrDefaultAsync(x => x.Slug == requestPath);
        if (entity == null)
        {
            return null;
        }

        return new RouteValueDictionary
        {
            { "controller", entity.EntityType.RoutingController },
            { "action", entity.EntityType.RoutingAction },
            { "id", entity.EntityId }
        };

    }
}

