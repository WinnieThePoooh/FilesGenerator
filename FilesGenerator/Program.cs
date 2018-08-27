using FilesGenerator.Logic;
using FilesGenerator.Logic.swea;

namespace FilesGenerator
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      var generator = new Generator(new FileContentGenerator(100, 100, 0));
      var rootFolder = @"C:\logs\generated";
      generator.Generate(rootFolder, 2, 2, 2);
    }
  }
}