using System.IO;

namespace FilesGenerator.Logic
{
  class FileCreator
  {
    public void Create(string path, string content)
    {
      using (var file = new StreamWriter(path))
      {
        file.WriteLine(content);
      }
    }
  }
}