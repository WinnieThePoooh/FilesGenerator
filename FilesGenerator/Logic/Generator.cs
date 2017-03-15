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
      int warningsCount,
      int todoCount)
    {
      GenerateSafety(
        rootFolder,
        filesInEachFolder,
        subfoldersInEachFolder,
        nestingLevel,
        errorsCount,
        warningsCount,
        todoCount,
        true,
        string.Empty);
    }

    private void GenerateSafety(
      string rootFolder,
      int filesInEachFolder,
      int subfoldersInEachFolder,
      int nestingLevel,
      int errorsCount,
      int warningsCount,
      int todoCount,
      bool shouldInit,
      string classNameSuffix)
    {
      if (shouldInit)
        myFolderCreator.Init(rootFolder);

      for (var i = 0; i < filesInEachFolder; i++)
      {
        var localClassNameSuffix = nestingLevel + "_" + i + "_"  + classNameSuffix;
        var filePath = rootFolder + @"\file" + localClassNameSuffix + ".cs";
        var content = myFileContentGenerator.Generate(localClassNameSuffix, errorsCount, warningsCount, todoCount);
        myFileCreator.Create(filePath, content);
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
          errorsCount,
          warningsCount,
          todoCount,
          false,
          classNameSuffix + folderSuffix);
      }
    }
  }
}