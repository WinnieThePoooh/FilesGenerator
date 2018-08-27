using System;
using System.Collections.Generic;
using System.Text;

namespace FilesGenerator.Logic.resources
{
    public class ProjectFileContentGenerator : IFileContentGenerator
    {
        private readonly string _projFileContent;
        private readonly string _resourceFileTemplate;
        private readonly string _resourceFileDesignerTemplate;
        private readonly string _solutionFileTemplate;
        private readonly ICollection<GeneratedProject> _generatedProjects;

        public ProjectFileContentGenerator()
        {
            _projFileContent = new ResourceReader().Read(GetTemplateFileName("ProjFileTemplate"));
            _resourceFileTemplate = new ResourceReader().Read(GetTemplateFileName("ResourceFileTemplate"));
            _resourceFileDesignerTemplate = new ResourceReader().Read(GetTemplateFileName("ResourceFileDesignerTemplate"));
            _solutionFileTemplate = new ResourceReader().Read(GetTemplateFileName("SlnFileTemplate"));
            _generatedProjects = new List<GeneratedProject>();
        }
        
        public IEnumerable<GeneratedFile> GenerateAfter(string rootFolder)
        {
            var uberProject = Generate("UberProject", rootFolder);
            
            var projects = new StringBuilder();
            var projectsConfigs = new StringBuilder();
            foreach (var proj in _generatedProjects)
            {
                projects.AppendLine(string.Format("Project(\"{0}\") = \"{1}\", \"{2}\", \"{3}\"", "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}", proj.Name, proj.Path, proj.Id));
                projects.AppendLine("EndProject");
                projectsConfigs.AppendLine(string.Format("{0}.Debug|Any CPU.ActiveCfg = Debug|Any CPU", "{" + proj.Id + "}"));
                projectsConfigs.AppendLine(string.Format("{0}.Debug|Any CPU.Build.0 = Debug|Any CPU", "{" + proj.Id + "}"));
            }

            var content = _solutionFileTemplate
                .Replace("$projects", projects.ToString())
                .Replace("$projectConfigs", projectsConfigs.ToString());
            var res = new List<GeneratedFile>();
            res.AddRange(uberProject);
            res.Add(new GeneratedFile("solutionGenerated.sln", content));
            return res;
        }

        public IEnumerable<GeneratedFile> Generate(string classSuffix, string folder)
        {
            var resourceFileName = "res" + classSuffix;
            var projectReferencesSb = new StringBuilder();
            foreach (var file in _generatedProjects)
                projectReferencesSb.AppendLine($"<ProjectReference Include=\"{file.Path}\" />");
            var projectId = Guid.NewGuid().ToString();
            var projectName = "GeneratedProject" + classSuffix;
            var projFileContent = _projFileContent
                .Replace("$projectId", projectId)
                .Replace("$rootNamespace", "Namespace" + classSuffix)
                .Replace("$assemblyName", projectName)
                .Replace("$ProjectReference", projectReferencesSb.ToString())
                .Replace("$resourceFileName", resourceFileName);
            var resourceItemName = "resource" + classSuffix + "ItemName";
            var resourceFileContent = _resourceFileTemplate
                .Replace("$resourceItemName", resourceItemName)
                .Replace("$resourceItemValue", "resource" + classSuffix + "Value");
            var resourceFileDesignerContent = _resourceFileDesignerTemplate
                .Replace("$resourceItemName", resourceItemName)
                .Replace("$resourceFileName", resourceFileName);
            
            var projFileName = "proj" + classSuffix + ".csproj";
            var projFileFullName = folder + @"\" + projFileName;
            _generatedProjects.Add(new GeneratedProject(projFileFullName, projectName, projectId));
            return new []
            {
                new GeneratedFile(projFileName, projFileContent),
                new GeneratedFile(resourceFileName + ".resx", resourceFileContent),
                new GeneratedFile(resourceFileName + ".Designer.cs", resourceFileDesignerContent)
            };
        }

        private static string GetTemplateFileName(string name)
        {
            return $"FilesGenerator.Logic.resources.{name}.txt";
        }

        private class GeneratedProject
        {
            public GeneratedProject(string path, string name, string id)
            {
                Path = path;
                Id = id;
                Name = name;
            }
            public string Path { get; }
            public string Name { get; }
            public string Id{ get; }
        }
    }
}