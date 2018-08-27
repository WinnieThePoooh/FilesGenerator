using System;
using System.Collections.Generic;

namespace FilesGenerator.Logic.resources
{
    public class ProjectFileContentGenerator : IFileContentGenerator
    {
        private readonly string _projFileContent;

        public ProjectFileContentGenerator()
        {
            _projFileContent = new ResourceReader().Read("FilesGenerator.Logic.resources.ProjFileContent.txt");
        }

        public IEnumerable<GeneratedFile> Generate(string classSuffix)
        {
            var content = _projFileContent
                .Replace("$projectId", Guid.NewGuid().ToString())
                .Replace("$rootNamespace", "Namespace" + classSuffix)
                .Replace("$assemblyName", "AssemblyName" + classSuffix)
                .Replace("$resourceFileName", "Resource" + classSuffix);
            return new [] { new GeneratedFile("proj" + classSuffix + ".csproj", content)};
        }
    }
}