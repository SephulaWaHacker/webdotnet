using BMW.CloudAdoption.BOM.Modules;

namespace BMW.CloudAdoption.BOM.Core.Extensions;

public static class ModuleExtensions
{
    public static IServiceCollection RegisterModules(this IServiceCollection services)
    {
        var modules = DiscoverModules();
        foreach (var module in modules)
        {
            module.RegisterModule(services);
            services.AddSingleton(module);
        }

        return services;
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var modules = app.Services.GetServices<IModule>();
        foreach (var module in modules)
            module.MapEndpoints(app);

        return app;
    }

    private static IEnumerable<IModule> DiscoverModules()
        => typeof(IModule).Assembly.GetTypes().Where(x => x.IsClass && x.IsAssignableTo(typeof(IModule)))
            .Select(Activator.CreateInstance)
            .Cast<IModule>();
}