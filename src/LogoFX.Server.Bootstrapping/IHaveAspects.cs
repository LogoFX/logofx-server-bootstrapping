// ReSharper disable once CheckNamespace
namespace Solid.Extensibility
{
    //TODO: Move to packages
    public interface IHaveAspects<T>
    {
        T UseAspect(IAspect aspect);
    }
}