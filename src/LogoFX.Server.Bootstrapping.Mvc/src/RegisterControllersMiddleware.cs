using LogoFX.Server.Bootstrapping.Common;
using Microsoft.Extensions.DependencyInjection;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.Middleware;

namespace LogoFX.Server.Bootstrapping.Mvc
{
    public class RegisterControllersMiddleware<TBootstrapper> : IMiddleware<TBootstrapper>
        where TBootstrapper : class, IAssemblySourceProvider, IHaveRegistrator<IServiceCollection>        
    {        
        public TBootstrapper Apply(TBootstrapper @object)
        {
            foreach (var assembly in @object.Assemblies)
            {
                @object.Registrator.RegisterControllers(assembly);
            }
            return @object;
        }
    }
}
