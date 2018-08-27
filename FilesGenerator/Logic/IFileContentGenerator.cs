using System.Collections.Generic;

namespace FilesGenerator.Logic
{
    public interface IFileContentGenerator
    {
        IEnumerable<GeneratedFile> Generate(string classSuffix);
    }
}