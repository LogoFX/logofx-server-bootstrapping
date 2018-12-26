using System.Collections.Generic;
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
    internal abstract class BootstrapperBase2 :
         IInitializable,
         IExtensible<BootstrapperBase2>,
         IExtensible<IHaveRegistrator<IServiceCollection>>,
         IHaveAspects<BootstrapperBase2>,
         ICompositionModulesProvider,
         IHaveRegistrator<IServiceCollection>
    {
        private ModularityAspect _modularityAspect;
        private DiscoveryAspect _discoveryAspect;
        private readonly ExtensibilityAspect<IHaveRegistrator<IServiceCollection>> _registratorExtensibilityAspect;
        private readonly ExtensibilityAspect<BootstrapperBase2> _concreteExtensibilityAspect;
        private readonly AspectsCollection _aspectsCollection = new AspectsCollection();

        protected BootstrapperBase2(IServiceCollection dependencyRegistrator) : this() =>
            Registrator = dependencyRegistrator;

        public IServiceCollection Registrator { get; }

        private BootstrapperBase2()
        {
            _concreteExtensibilityAspect = new ExtensibilityAspect<BootstrapperBase2>(this);
            _registratorExtensibilityAspect = new ExtensibilityAspect<IHaveRegistrator<IServiceCollection>>(this);
        }

        IEnumerable<ICompositionModule> ICompositionModulesProvider<ICompositionModule>.Modules =>
            _modularityAspect.Modules;

        public BootstrapperBase2 Use(IMiddleware<BootstrapperBase2> middleware)
        {
            return _concreteExtensibilityAspect.Use(middleware);
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

        public BootstrapperBase2 UseAspect(IAspect aspect)
        {
            _aspectsCollection.UseAspect(aspect);
            return this;
        }

        public IHaveRegistrator<IServiceCollection> Use(IMiddleware<IHaveRegistrator<IServiceCollection>> middleware)
        {
            return _registratorExtensibilityAspect.Use(middleware);            
        }
    }
}
