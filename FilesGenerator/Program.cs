using FilesGenerator.Logic;
using FilesGenerator.Logic.resources;
using FilesGenerator.Logic.swea;

namespace FilesGenerator
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      //var fileContentGenerator = new FileContentGenerator(5, 5, 5);
      var fileContentGenerator = new ProjectFileContentGenerator();
      var generator = new Generator(fileContentGenerator);
      var rootFolder = @"C:\logs\generated";
      generator.Generate(rootFolder, 1, 1, 1);
    }
  }
}
