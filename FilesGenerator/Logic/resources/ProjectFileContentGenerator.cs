namespace FilesGenerator.Logic.resources
{
    public class ProjectFileContentGenerator : IFileContentGenerator
    {
        private readonly string _projFileContent;

        public ProjectFileContentGenerator()
        {
            _projFileContent = new ResourceReader().Read("FilesGenerator.Logic.resources.ProjFileContent.txt");
        }

        public string Generate(string classSuffix)
        {
            return _projFileContent;
        }
    }
}