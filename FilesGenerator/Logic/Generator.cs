namespace FilesGenerator.Logic
{
  public class Generator
  {
    private readonly IFileContentGenerator _fileContentGenerator;
    private readonly FileCreator myFileCreator = new FileCreator();
    private readonly FolderCreator myFolderCreator = new FolderCreator();

    public Generator(IFileContentGenerator fileContentGenerator)
    {
      _fileContentGenerator = fileContentGenerator;
    }
    
    public void Generate(
      string rootFolder,
      int filesInEachFolder,
      int subfoldersInEachFolder,
      int nestingLevel)
    {
      GenerateSafety(
        rootFolder,
        filesInEachFolder,
        subfoldersInEachFolder,
        nestingLevel,
        true,
        string.Empty);
      
      var files = _fileContentGenerator.GenerateAfter(rootFolder);
      foreach (var file in files)
        CreateFile(rootFolder, file);
    }

    private void GenerateSafety(
      string rootFolder,
      int filesInEachFolder,
      int subfoldersInEachFolder,
      int nestingLevel,
      bool shouldInit,
      string classNameSuffix)
    {
      if (shouldInit)
        myFolderCreator.Init(rootFolder);

      for (var i = 0; i < filesInEachFolder; i++)
      {
        var localClassNameSuffix = nestingLevel + "_" + i + "_"  + classNameSuffix;
        
        var files = _fileContentGenerator.Generate(localClassNameSuffix, rootFolder);
        foreach (var file in files)
        {
          CreateFile(rootFolder, file);
        }
      }

      if (nestingLevel == 0)
        return;

      for (var i = 0; i < subfoldersInEachFolder; i++)
      {
        var folderSuffix = nestingLevel + "_" + i;
        var folderPath = rootFolder + @"\folder" + folderSuffix;
        myFolderCreator.Create(folderPath);
        GenerateSafety(
          folderPath,
          filesInEachFolder,
          subfoldersInEachFolder,
          nestingLevel - 1,
          false,
          classNameSuffix + folderSuffix);
      }
    }

    private void CreateFile(string rootFolder, GeneratedFile file)
    {
      var filePath = rootFolder + @"\" + file.Name;
      myFileCreator.Create(filePath, file.Content);
    }
  }
}