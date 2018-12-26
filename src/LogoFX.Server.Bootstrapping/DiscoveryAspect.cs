using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Solid.Common;
using Solid.Extensibility;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.Composition.Web;

public class DiscoveryAspect : IAspect, IAssemblySourceProvider
{
    private readonly ModularityOptions _compositionOptions;

    public DiscoveryAspect(ModularityOptions compositionOptions)
    {
        _compositionOptions = compositionOptions;
    }

    public void Initialize()
    {
        GetAssemblies();
    }

    public string Id => "Discovery";
    public string[] Dependencies => new[] { "Modularity", "Platform" };

    private Assembly[] _assemblies;
    /// <inheritdoc />       
    public IEnumerable<Assembly> Assemblies => _assemblies ?? (_assemblies = CreateAssemblies());

    private Assembly[] CreateAssemblies()
    {
        return GetAssemblies();
    }

    private Assembly[] GetAssemblies()
    {        
        var rootPath = PlatformProvider.Current.GetAbsolutePath(_compositionOptions.ModulesPath);
        var assembliesResolver = new AssembliesResolver(
            new ServerAssemblySourceProvider(rootPath));
        return ((IAssembliesReadOnlyResolver)assembliesResolver).GetAssemblies().ToArray();
    }
}