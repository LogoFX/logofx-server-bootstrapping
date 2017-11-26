using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Solid.Practices.Composition;
using Solid.Practices.Composition.Contracts;

namespace LogoFX.Server.Bootstrapping
{    
    partial class BootstrapperBase : IAssemblySourceProvider
    {        
        private Assembly[] _assemblies;
        /// <summary>
        /// Gets the assemblies that will be inspected for the application components.
        /// </summary>
        /// <value>
        /// The assemblies.
        /// </value>
        public IEnumerable<Assembly> Assemblies
        {
            get { return _assemblies ?? (_assemblies = CreateAssemblies()); }
        }

        private Assembly[] CreateAssemblies()
        {
            return GetAssemblies();
        }

        private Assembly[] GetAssemblies()
        {
            OnConfigureAssemblyResolution();
            var rootPath = PathHelper.GetAbsolutePath(ModulesPath);
            var assembliesResolver = new AssembliesResolver(
                new ServerAssemblySourceProvider(rootPath));
            return ((IAssembliesReadOnlyResolver)assembliesResolver).GetAssemblies().ToArray();
        }        

        /// <summary>
        /// Override this to provide custom assembly namespaces collection.
        /// </summary>
        protected virtual void OnConfigureAssemblyResolution()
        {
        }
    }
}
