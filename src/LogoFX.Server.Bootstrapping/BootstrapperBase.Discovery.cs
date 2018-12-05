﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Solid.Common;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.Composition.Web;

namespace LogoFX.Server.Bootstrapping
{    
    partial class BootstrapperBase : IAssemblySourceProvider
    {        
        private Assembly[] _assemblies;
        /// <inheritdoc />       
        public IEnumerable<Assembly> Assemblies => _assemblies ?? (_assemblies = CreateAssemblies());

        private Assembly[] CreateAssemblies()
        {
            return GetAssemblies();
        }

        private Assembly[] GetAssemblies()
        {
            OnConfigureAssemblyResolution();
            var rootPath = PlatformProvider.Current.GetAbsolutePath(ModulesPath);
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
