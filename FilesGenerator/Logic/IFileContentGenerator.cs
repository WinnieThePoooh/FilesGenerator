using System.Collections.Generic;

namespace FilesGenerator.Logic
{
    public interface IFileContentGenerator
    {
        IEnumerable<GeneratedFile> Generate(string classSuffix, string folder);
        IEnumerable<GeneratedFile> GenerateAfter(string rootFolder);
    }
}