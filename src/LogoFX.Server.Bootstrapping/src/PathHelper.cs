using System.IO;
using Solid.Practices.Composition;

namespace LogoFX.Server.Bootstrapping
{
    internal static class PathHelper
    {
        internal static string GetAbsolutePath(string relativePath)
        {
            return Path.Combine(PlatformProvider.Current.GetRootPath(), relativePath);
        }
    }
}
