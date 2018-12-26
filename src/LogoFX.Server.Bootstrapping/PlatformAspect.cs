using Solid.Extensibility;

// ReSharper disable once CheckNamespace
namespace Solid.Common
{
    //TODO: Move to packages
    public class PlatformAspect : IAspect
    {
        public void Initialize()
        {
            PlatformProvider.Current = new NetStandardPlatformProvider();
        }

        public string Id => "Platform";
        public string[] Dependencies => new string[] { };
    }
}