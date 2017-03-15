namespace FilesGenerator.Logic
{
  public class Generator
  {
    private readonly FileContentGenerator myFileContentGenerator = new FileContentGenerator();
    private readonly FileCreator myFileCreator = new FileCreator();
    private readonly FolderCreator myFolderCreator = new FolderCreator();

    public void Generate(
      string rootFolder,
      int filesInEachFolder,
      int subfoldersInEachFolder,
      int nestingLevel,
      int errorsCount,
      int warningsCount)
    {
      GenerateSafety(
        rootFolder,
        filesInEachFolder,
        subfoldersInEachFolder,
        nestingLevel,
        errorsCount,
        warningsCount,
        true);
    }

    private void GenerateSafety(
      string rootFolder,
      int filesInEachFolder,
      int subfoldersInEachFolder,
      int nestingLevel,
      int errorsCount,
      int warningsCount,
      bool shouldInit)
    {
      if (shouldInit)
        myFolderCreator.Init(rootFolder);

      for (var i = 0; i < filesInEachFolder; i++)
      {
        var classNameSuffix = nestingLevel + "_" + i;
        var filePath = rootFolder + @"\file" + classNameSuffix + ".cs";
        var content = myFileContentGenerator.Generate(classNameSuffix, errorsCount, warningsCount);
        myFileCreator.Create(filePath, content);
      }

      if (nestingLevel == 0)
        return;

      for (var i = 0; i < subfoldersInEachFolder; i++)
      {
        var folderPath = rootFolder + @"\folder" + nestingLevel + "_" + i;
        myFolderCreator.Create(folderPath);
        GenerateSafety(
          folderPath,
          filesInEachFolder,
          subfoldersInEachFolder,
          nestingLevel - 1,
          errorsCount,
          warningsCount,
          false);
      }
    }
  }
}