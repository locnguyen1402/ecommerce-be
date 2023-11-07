namespace Products.MinimalApis.Modules;
public interface IModule
{
    abstract void MapEndpoints(IEndpointRouteBuilder endpoints);
}

