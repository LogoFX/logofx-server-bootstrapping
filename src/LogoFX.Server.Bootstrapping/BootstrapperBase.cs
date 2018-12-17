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
    public partial class BootstrapperBase : 
        IInitializable,        
        IHaveRegistrator<IServiceCollection>,
        IHaveErrors
    {       
        /// <inheritdoc />
        protected BootstrapperBase(IServiceCollection dependencyRegistrator)
        {
            Registrator = dependencyRegistrator;
            PlatformProvider.Current = new NetStandardPlatformProvider();
            _concreteMiddlewaresWrapper = new MiddlewaresWrapper<BootstrapperBase>(this);
            _registratorMiddlewaresWrapper = new MiddlewaresWrapper<IHaveRegistrator<IServiceCollection>>(this);
        }

        /// <inheritdoc />
        public IServiceCollection Registrator { get; }       

        /// <inheritdoc />
        public void Initialize()
        {            
            CreateAssemblies();
            InitializeCompositionModules();
            MiddlewareApplier.ApplyMiddlewares(this, _concreteMiddlewaresWrapper.Middlewares);
            MiddlewareApplier.ApplyMiddlewares(this, _registratorMiddlewaresWrapper.Middlewares);
        }
    }        
}
