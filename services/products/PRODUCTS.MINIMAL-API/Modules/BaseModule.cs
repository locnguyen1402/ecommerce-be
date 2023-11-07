namespace Products.MinimalApis.Modules;
public abstract class BaseModule : IModule
{
    public BaseModule(IEndpointRouteBuilder builder, string path)
    {
        var routeGroup = builder.MapGroup(path);
        MapEndpoints(routeGroup);
    }
    public abstract void MapEndpoints(IEndpointRouteBuilder builder);
}