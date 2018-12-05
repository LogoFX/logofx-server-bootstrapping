using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Solid.Bootstrapping;
using Solid.Common;
using Solid.Extensibility;
using Solid.Practices.Middleware;

namespace LogoFX.Server.Bootstrapping
{
    /// <summary>
    /// Defines basic bootstrapper for server initialization.
    /// </summary>
    public partial class BootstrapperBase : IInitializable,
        IExtensible<BootstrapperBase>,     
        IHaveRegistrator<IServiceCollection>,
        IHaveErrors
    {
        private readonly
            List<IMiddleware<BootstrapperBase>>
            _middlewares =
                new List<IMiddleware<BootstrapperBase>>();

        /// <inheritdoc />
        protected BootstrapperBase(IServiceCollection dependencyRegistrator)
        {
            Registrator = dependencyRegistrator;
            PlatformProvider.Current = new NetStandardPlatformProvider();
        }

        /// <inheritdoc />
        public IServiceCollection Registrator { get; }

        /// <inheritdoc />
        public BootstrapperBase Use(
            IMiddleware<BootstrapperBase> middleware)
        {
            _middlewares.Add(middleware);
            return this;
        }

        /// <inheritdoc />
        public void Initialize()
        {            
            CreateAssemblies();
            InitializeCompositionModules();
            MiddlewareApplier.ApplyMiddlewares(this, _middlewares);
        }
    }        
}
