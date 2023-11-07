using Products.MinimalApis.Modules;

namespace Products.MinimalApis.Extensions;

public static class ModuleExtensions
{
    public static void AutoMapEndpoints(this WebApplication app)
    {
        typeof(IModule).Assembly
            .GetTypes()
            .Where(p => !p.IsAbstract && p.IsClass && p.IsAssignableTo(typeof(IModule)))
            .ToList()
            .ForEach(type => Activator.CreateInstance(type, app));
    }
}
