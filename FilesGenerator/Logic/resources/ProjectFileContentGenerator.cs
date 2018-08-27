using System;
using System.Collections.Generic;

namespace FilesGenerator.Logic.resources
{
    public class ProjectFileContentGenerator : IFileContentGenerator
    {
        private readonly string _projFileContent;
        private readonly string _resourceFileTemplate;
        private string _resourceFileDesignerTemplate;

        public ProjectFileContentGenerator()
        {
            _projFileContent = new ResourceReader().Read("FilesGenerator.Logic.resources.ProjFileTemplate.txt");
            _resourceFileTemplate = new ResourceReader().Read("FilesGenerator.Logic.resources.ResourceFileTemplate.txt");
            _resourceFileDesignerTemplate = new ResourceReader().Read("FilesGenerator.Logic.resources.ResourceFileDesignerTemplate.txt");
        }

        public IEnumerable<GeneratedFile> Generate(string classSuffix)
        {
            var resourceFileName = "res" + classSuffix;
            var projFileContent = _projFileContent
                .Replace("$projectId", Guid.NewGuid().ToString())
                .Replace("$rootNamespace", "Namespace" + classSuffix)
                .Replace("$assemblyName", "AssemblyName" + classSuffix)
                .Replace("$ProjectReference", string.Empty)
                .Replace("$resourceFileName", resourceFileName);
            var resourceItemName = "resource" + classSuffix + "ItemName";
            var resourceFileContent = _resourceFileTemplate
                .Replace("$resourceItemName", resourceItemName)
                .Replace("$resourceItemValue", "resource" + classSuffix + "Value");
            var resourceFileDesignerContent = _resourceFileDesignerTemplate
                .Replace("$resourceItemName", resourceItemName);
            return new []
            {
                new GeneratedFile("proj" + classSuffix + ".csproj", projFileContent),
                new GeneratedFile(resourceFileName + ".resx", resourceFileContent),
                new GeneratedFile(resourceFileName + ".Designer.cs", resourceFileDesignerContent)
            };
        }
    }
}