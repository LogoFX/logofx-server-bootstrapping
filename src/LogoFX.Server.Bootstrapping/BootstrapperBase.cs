using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Solid.Bootstrapping;
using Solid.Common;
using Solid.Extensibility;
using Solid.Practices.Composition;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.Middleware;
using Solid.Practices.Modularity;

namespace LogoFX.Server.Bootstrapping
{
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
        private readonly AspectsCollection _aspectsCollection = new AspectsCollection();

        protected BootstrapperBase(IServiceCollection dependencyRegistrator) : this() =>
            Registrator = dependencyRegistrator;

        public IServiceCollection Registrator { get; }

        private BootstrapperBase()
        {
            _concreteExtensibilityAspect = new ExtensibilityAspect<BootstrapperBase>(this);
            _registratorExtensibilityAspect = new ExtensibilityAspect<IHaveRegistrator<IServiceCollection>>(this);
        }

        public IEnumerable<Assembly> Assemblies => _discoveryAspect.Assemblies;

        IEnumerable<ICompositionModule> ICompositionModulesProvider<ICompositionModule>.Modules =>
            _modularityAspect.Modules;

        public BootstrapperBase Use(IMiddleware<BootstrapperBase> middleware)
        {
            return _concreteExtensibilityAspect.Use(middleware);
        }

        public IHaveRegistrator<IServiceCollection> Use(IMiddleware<IHaveRegistrator<IServiceCollection>> middleware)
        {
            return _registratorExtensibilityAspect.Use(middleware);
        }
        
        public virtual ModularityOptions ModularityOptions => new ModularityOptions();

        public void Initialize()
        {
            _aspectsCollection.UseCoreAspects(CreateCoreAspects());
            _aspectsCollection.Initialize();
        }

        private IEnumerable<IAspect> CreateCoreAspects()
        {
            var aspects = new List<IAspect> { new PlatformAspect() };
            _modularityAspect = new ModularityAspect(ModularityOptions);
            aspects.Add(_modularityAspect);
            _discoveryAspect = new DiscoveryAspect(ModularityOptions);
            aspects.Add(_discoveryAspect);
            aspects.Add(_concreteExtensibilityAspect);
            aspects.Add(_registratorExtensibilityAspect);
            return aspects;
        }

        public BootstrapperBase UseAspect(IAspect aspect)
        {
            _aspectsCollection.UseAspect(aspect);
            return this;
        }        
    }
}
