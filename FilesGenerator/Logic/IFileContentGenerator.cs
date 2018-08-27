namespace FilesGenerator.Logic
{
    public interface IFileContentGenerator
    {
        GeneratedFile Generate(string classSuffix);
    }
}