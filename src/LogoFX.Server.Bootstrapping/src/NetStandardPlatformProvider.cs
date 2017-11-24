using System.IO;
using Solid.Practices.Composition.Contracts;

namespace LogoFX.Server.Bootstrapping
{
    class NetStandardPlatformProvider : IPlatformProvider
    {
        /// <summary>Gets the files at the specified path.</summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public string[] GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }

        /// <summary>
        /// Gets the files at the specified path, using provided search pattern.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns></returns>
        public string[] GetFiles(string path, string searchPattern)
        {
            return Directory.GetFiles(path, searchPattern);
        }

        /// <summary>Gets the root path.</summary>
        /// <returns></returns>
        public string GetRootPath()
        {
            return Directory.GetCurrentDirectory();
        }

        /// <summary>
        /// Writes the specified text into the resource identified by the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="contents">The text.</param>
        public void WriteText(string path, string contents)
        {
            using (StreamWriter streamWriter = new StreamWriter((Stream)new FileStream(path, FileMode.Create)))
            {
                streamWriter.Write(contents);
                streamWriter.Flush();
            }
        }

        /// <summary>
        /// Reads the contents of the resource identified by the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public string ReadText(string path)
        {
            using (StreamReader streamReader = new StreamReader((Stream)new FileStream(path, FileMode.Open)))
                return streamReader.ReadToEnd();
        }
    }
}
