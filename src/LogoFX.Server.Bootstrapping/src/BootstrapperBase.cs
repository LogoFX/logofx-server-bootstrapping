using System.Collections.Generic;
using LogoFX.Bootstrapping;
using Microsoft.Extensions.DependencyInjection;
using Solid.Bootstrapping;
using Solid.Extensibility;
using Solid.Practices.Composition;
using Solid.Practices.Middleware;

namespace LogoFX.Server.Bootstrapping
{
    public partial class BootstrapperBase : IInitializable,
        IExtensible<BootstrapperBase>,     
        IHaveRegistrator<IServiceCollection>,
        IHaveErrors
    {
        private readonly
            List<IMiddleware<BootstrapperBase>>
            _middlewares =
                new List<IMiddleware<BootstrapperBase>>();

        protected BootstrapperBase(IServiceCollection dependencyRegistrator)
        {
            Registrator = dependencyRegistrator;
            PlatformProvider.Current = new NetStandardPlatformProvider();
        }

        public IServiceCollection Registrator { get; }       

        /// <summary>
        /// Extends the functionality by using the specified middleware.
        /// </summary>
        /// <param name="middleware">The middleware.</param>
        /// <returns></returns>
        public BootstrapperBase Use(
            IMiddleware<BootstrapperBase> middleware)
        {
            _middlewares.Add(middleware);
            return this;
        }

        public void Initialize()
        {
            AssemblyLoadingManager.ServerNamespaces = () => new[] {"Api", "Facade"};
            CreateAssemblies();
            InitializeCompositionModules();
            MiddlewareApplier.ApplyMiddlewares(this, _middlewares);
        }
    }        
}
