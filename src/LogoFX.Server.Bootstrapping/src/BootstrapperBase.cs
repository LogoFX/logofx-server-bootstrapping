using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Solid.Bootstrapping;
using Solid.Extensibility;
using Solid.Practices.Composition;
using Solid.Practices.Middleware;

namespace LogoFX.Server.Bootstrapping
{
    public partial class BootstrapperBase : IInitializable,
        IExtensible<BootstrapperBase>,     
        IHaveRegistrator<IServiceCollection>
    {
        private readonly
            List<IMiddleware<BootstrapperBase>>
            _middlewares =
                new List<IMiddleware<BootstrapperBase>>();

        protected BootstrapperBase(IServiceCollection dependencyRegistrator)
        {
            Registrator = dependencyRegistrator;
            PlatformProvider.Current =
#if NET461
                new NetPlatformProvider();
#elif NETSTANDARD1_6 || NETSTANDARD2_0
                new NetStandardPlatformProvider();
#endif
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
            AssemblyLoadingManager.ServerNamespaces = () => new[] {"Api"};
            CreateAssemblies();
            InitializeCompositionModules();
            MiddlewareApplier.ApplyMiddlewares(this, _middlewares);
        }
    }        
}
