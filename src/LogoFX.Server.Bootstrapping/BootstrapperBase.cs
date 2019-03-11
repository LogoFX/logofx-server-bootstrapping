using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Solid.Bootstrapping;
using Solid.Common;
using Solid.Core;
using Solid.Extensibility;
using Solid.Practices.Composition;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.Middleware;
using Solid.Practices.Modularity;

namespace LogoFX.Server.Bootstrapping
{
    /// <summary>
    /// Base bootstrapper for server/back-end
    /// </summary>
    public class BootstrapperBase :
         IInitializable,
         IExtensible<BootstrapperBase>,
         IExtensible<IHaveRegistrator<IServiceCollection>>,
         IHaveAspects<BootstrapperBase>,
         ICompositionModulesProvider,
         IHaveRegistrator<IServiceCollection>, 
         IAssemblySourceProvider
    {
        private ModularityAspect _modularityAspect;
        private DiscoveryAspect _discoveryAspect;
        private readonly ExtensibilityAspect<IHaveRegistrator<IServiceCollection>> _registratorExtensibilityAspect;
        private readonly ExtensibilityAspect<BootstrapperBase> _concreteExtensibilityAspect;
        private readonly AspectsWrapper _aspectsWrapper = new AspectsWrapper();

        /// <summary>
        /// Creates an instance of <see cref="BootstrapperBase"/>
        /// </summary>
        /// <param name="dependencyRegistrator"></param>
        protected BootstrapperBase(IServiceCollection dependencyRegistrator) : this() =>
            Registrator = dependencyRegistrator;

        /// <inheritdoc />
        public IServiceCollection Registrator { get; }

        private BootstrapperBase()
        {
            _concreteExtensibilityAspect = new ExtensibilityAspect<BootstrapperBase>(this);
            _registratorExtensibilityAspect = new ExtensibilityAspect<IHaveRegistrator<IServiceCollection>>(this);
        }

        /// <inheritdoc />
        public IEnumerable<Assembly> Assemblies => _discoveryAspect.Assemblies;

        IEnumerable<ICompositionModule> ICompositionModulesProvider<ICompositionModule>.Modules =>
            _modularityAspect.Modules;

        /// <inheritdoc />
        public BootstrapperBase Use(IMiddleware<BootstrapperBase> middleware)
        {
            return _concreteExtensibilityAspect.Use(middleware);
        }

        /// <inheritdoc />
        public IHaveRegistrator<IServiceCollection> Use(IMiddleware<IHaveRegistrator<IServiceCollection>> middleware)
        {
            return _registratorExtensibilityAspect.Use(middleware);
        }
        
        /// <summary>
        /// Override to provide custom composition options.
        /// </summary>
        public virtual CompositionOptions CompositionOptions => new CompositionOptions();

        /// <inheritdoc />
        public void Initialize()
        {
            _aspectsWrapper.UseCoreAspects(CreateCoreAspects());
            _aspectsWrapper.Initialize();
        }

        private IEnumerable<IAspect> CreateCoreAspects()
        {
            var aspects = new List<IAspect> { new PlatformAspect() };
            _discoveryAspect = new DiscoveryAspect(CompositionOptions);
            _modularityAspect = new ModularityAspect(_discoveryAspect, CompositionOptions);
            aspects.Add(_modularityAspect);
            aspects.Add(_discoveryAspect);
            aspects.Add(_concreteExtensibilityAspect);
            aspects.Add(_registratorExtensibilityAspect);
            return aspects;
        }

        /// <inheritdoc />
        public BootstrapperBase UseAspect(IAspect aspect)
        {
            _aspectsWrapper.UseAspect(aspect);
            return this;
        }        
    }
}
