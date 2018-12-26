using System;
using System.Collections.Generic;
using Solid.Practices.Modularity;

// ReSharper disable once CheckNamespace
namespace Solid.Practices.Composition.Contracts
{
    //TODO: Move to Composition.Contracts
    public class ModularityOptions
    {
        public string ModulesPath { get; set; } = ".";

        public string[] Prefixes { get; set; } = { };
    }

    //TODO: Move to Composition.Contracts
    public struct ModularityInfo
    {
        /// <summary>
        /// Gets the collection of <see cref="T:Solid.Practices.Modularity.ICompositionModule" />.
        /// </summary>
        public IEnumerable<ICompositionModule> Modules { get; internal set; }

        /// <summary>Gets the collection of composition errors.</summary>
        public IEnumerable<Exception> Errors { get; internal set; }
    }
}