using System.IO;
using System.Reflection;

namespace FilesGenerator.Logic
{
    public class ResourceReader
    {
        public string Read(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }
    }
}