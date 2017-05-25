using FilesGenerator.Logic;

namespace FilesGenerator
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      var generator = new Generator();
      var rootFolder = @"C:\logs\generated";
      generator.Generate(rootFolder, 3, 3, 8, 100, 100, 0);
    }
  }
}
