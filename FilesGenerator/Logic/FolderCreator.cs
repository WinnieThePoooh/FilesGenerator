using System.IO;

namespace FilesGenerator.Logic
{
  class FolderCreator
  {
    public void Init(string path)
    {
      Directory.Delete(path, true);
      Directory.CreateDirectory(path);
    }

    public void Create(string path)
    {
      
    }
  }
}