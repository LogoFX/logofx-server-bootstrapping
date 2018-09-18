using Microsoft.Extensions.DependencyInjection;
using Solid.Bootstrapping;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.Middleware;

namespace LogoFX.Server.Bootstrapping.Mvc
{
    public class RegisterCoreMiddleware<TBootstrapper> : IMiddleware<TBootstrapper>
        where TBootstrapper : class, IAssemblySourceProvider, IHaveRegistrator<IServiceCollection>
    {
        public TBootstrapper Apply(TBootstrapper @object)
        {
            @object.Registrator
                   .AddMvcCore()
                   .AddApiExplorer()
                   .AddJsonFormatters();
            return @object;
        }
    }
}
