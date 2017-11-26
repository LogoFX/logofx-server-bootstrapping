using LogoFX.Bootstrapping;
using Solid.Bootstrapping;
using Solid.Extensibility;
using Solid.Practices.Composition.Contracts;

namespace LogoFX.Server.Bootstrapping
{
    public static class BootstrapperExtensions
    {        
        public static TBootstrapper UseCompositionModules<TBootstrapper, TDependencyRegistrator>(
            this TBootstrapper bootstrapper)
            where TBootstrapper : class, IExtensible<TBootstrapper>,
            IHaveRegistrator<TDependencyRegistrator>, ICompositionModulesProvider
            where TDependencyRegistrator : class
        {
            return bootstrapper.Use(new RegisterCustomCompositionModulesMiddleware<TBootstrapper, TDependencyRegistrator>());
        }
    }
}
