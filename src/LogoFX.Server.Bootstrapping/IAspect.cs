using Solid.Bootstrapping;
using Solid.Core;

// ReSharper disable once CheckNamespace
namespace Solid.Extensibility
{
    //TODO: Move to packages
    public interface IAspect : IInitializable, IHaveDependencies
    {
        string Id { get; }
    }
}