using System.Text;

namespace FilesGenerator.Logic.swea
{
  internal class FileContentGenerator : IFileContentGenerator
  {
    private readonly int _errorsCount;
    private readonly int _warningsCount;
    private readonly int _todoCount;

    public FileContentGenerator(int errorsCount, int warningsCount, int todoCount)
    {
      _errorsCount = errorsCount;
      _warningsCount = warningsCount;
      _todoCount = todoCount;
    }
    public string Generate(string classSuffix)
    {
      var builder = new StringBuilder();
      builder.AppendLine("namespace GeneratedFiles" + classSuffix);
      builder.AppendLine("{");
      builder.AppendLine("  class MyClass" + classSuffix);
      builder.AppendLine("  {");
      builder.AppendLine("    public void MyMethod()");
      builder.AppendLine("    {");
      for (int i = 0; i < _warningsCount; i++)
        builder.AppendLine("      var myWarning" + i + " = 0;");
      for (int i = 0; i < _errorsCount; i++)
        builder.AppendLine("      myError" + i + ";");
      for (int i = 0; i < _todoCount; i++)
        builder.AppendLine("      //todo " + i + "");
      builder.AppendLine("    }");
      builder.AppendLine("  }");
      builder.AppendLine("]");

      return builder.ToString();
    }
  }
}