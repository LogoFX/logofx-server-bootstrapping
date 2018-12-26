using System;
using System.Collections.Generic;
using System.Linq;
using Solid.Bootstrapping;
using Solid.Extensibility;
using Solid.Practices.Composition.Container;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.Modularity;

// ReSharper disable once CheckNamespace
namespace Solid.Practices.Composition
{
    //TODO: Move to packages
    public class ModularityAspect :
        IAspect,
        ICompositionModulesProvider,
        IHaveErrors
    {
        private readonly ModularityOptions _options;

        public ModularityAspect() :
            this(new ModularityOptions())
        {

        }

        public ModularityAspect(ModularityOptions options)
        {
            _options = options;
        }

        public IEnumerable<ICompositionModule> Modules { get; private set; } = new ICompositionModule[] { };
        public IEnumerable<Exception> Errors { get; private set; } = new Exception[] { };

        public string ModulesPath => _options.ModulesPath;
        public string[] Prefixes => _options.Prefixes;

        public void Initialize()
        {
            CompositionManager compositionManager = new CompositionManager();
            ModularityInfo modularityInfo = new ModularityInfo();
            try
            {
                compositionManager.Initialize(ModulesPath, Prefixes);
            }
            catch (AggregateAssemblyInspectionException ex)
            {
                modularityInfo.Errors = ex.InnerExceptions;
            }
            finally
            {
                modularityInfo.Modules = compositionManager.Modules == null ? new ICompositionModule[0] : compositionManager.Modules.ToArray<ICompositionModule>();
            }

            Modules = modularityInfo.Modules;
            Errors = modularityInfo.Errors;
        }

        public string Id => "Modularity";
        public string[] Dependencies => new[] { "Platform" };
    }
}