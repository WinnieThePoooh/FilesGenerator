using System.Text;

namespace FilesGenerator.Logic
{
  internal class FileContentGenerator
  {
    public string Generate(string classSuffix, int errorsCount, int warningsCount)
    {
      var builder = new StringBuilder();
      builder.AppendLine("namespace GeneratedFiles" + classSuffix);
      builder.AppendLine("{");
      builder.AppendLine("  class MyClass" + classSuffix);
      builder.AppendLine("  {");
      builder.AppendLine("    public void MyMethod()");
      builder.AppendLine("    {");
      for (int i = 0; i < warningsCount; i++)
      {
        builder.AppendLine("      var myWarning" + i + " = 0;");
      }
      for (int i = 0; i < errorsCount; i++)
      {
        builder.AppendLine("      myError" + i + ";");
      }
      builder.AppendLine("    }");
      builder.AppendLine("  }");
      builder.AppendLine("]");

      return builder.ToString();
    }
  }
}