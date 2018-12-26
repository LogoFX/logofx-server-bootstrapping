// ReSharper disable once CheckNamespace
namespace Solid.Core
{
    //TODO: Move to packages
    public interface IHaveDependencies
    {
        string[] Dependencies { get; }
    }
}