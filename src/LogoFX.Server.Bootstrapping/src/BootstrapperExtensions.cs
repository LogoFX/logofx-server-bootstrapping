using System;
using LogoFX.Bootstrapping;
using Solid.Bootstrapping;
using Solid.Extensibility;
using Solid.Practices.Composition.Contracts;

namespace LogoFX.Server.Bootstrapping
{
    /// <summary>
    /// Defines extensions for bootstrapper
    /// </summary>
    [Obsolete("To be replaced by LogoFX.Bootstrapping")]
    public static class BootstrapperExtensions
    {        
        /// <summary>
        /// Allows using composition modules for custom dependency registrator types.
        /// </summary>
        /// <typeparam name="TBootstrapper">The type of the bootstrapper.</typeparam>
        /// <typeparam name="TDependencyRegistrator">The type of the dependency registrator.</typeparam>
        /// <param name="bootstrapper">The bootstrapper.</param>
        /// <returns></returns>
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
