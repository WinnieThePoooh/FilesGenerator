namespace FilesGenerator.Logic
{
    public class GeneratedFile
    {
        public GeneratedFile(string name, string content)
        {
            Name = name;
            Content = content;
        }
        public string Name { get; }
        public string Content { get; }
    }
}