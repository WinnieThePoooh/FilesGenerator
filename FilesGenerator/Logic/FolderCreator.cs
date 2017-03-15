using System.IO;

namespace FilesGenerator.Logic
{
  class FolderCreator
  {
    public void Init(string path)
    {
      if (Directory.Exists(path))
        Directory.Delete(path, true);

      Create(path);
    }

    public void Create(string path)
    {
      Directory.CreateDirectory(path);
    }
  }
}