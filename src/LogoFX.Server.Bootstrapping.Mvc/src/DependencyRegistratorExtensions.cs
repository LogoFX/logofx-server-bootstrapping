using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace LogoFX.Server.Bootstrapping.Mvc
{
    public static class DependencyRegistratorExtensions
    {        
        public static IServiceCollection RegisterControllers(this IServiceCollection dependencyRegistrator, Assembly assembly)
        {
            return dependencyRegistrator.AddMvcCore()
                .AddApplicationPart(assembly)
                .AddControllersAsServices().Services;
        }
    }
}