using System;
using System.Collections.Generic;

namespace FilesGenerator.Logic.resources
{
    public class ProjectFileContentGenerator : IFileContentGenerator
    {
        private readonly string _projFileContent;
        private readonly string _resourceFileTemplate;
        private readonly string _resourceFileDesignerTemplate;

        public ProjectFileContentGenerator()
        {
            _projFileContent = new ResourceReader().Read(GetTemplateFileName("ProjFileTemplate"));
            _resourceFileTemplate = new ResourceReader().Read(GetTemplateFileName("ResourceFileTemplate"));
            _resourceFileDesignerTemplate = new ResourceReader().Read(GetTemplateFileName("ResourceFileDesignerTemplate"));
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

        private static string GetTemplateFileName(string name)
        {
            return $"FilesGenerator.Logic.resources.{name}.txt";
        }
    }
}