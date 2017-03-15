namespace FilesGenerator.Logic
{
  public class Generator
  {
    private readonly FolderCreator myFolderCreator = new FolderCreator();
    private readonly FileCreator myFileCreator = new FileCreator();
    private readonly FileContentGenerator myFileContentGenerator = new FileContentGenerator();

    public void Generate(
      string rootFolder,
      int filesInEachFolder,
      int subfoldersInEachFolder,
      int nestingLevel,
      int errorsCount,
      int warningsCount)
    {
      GenerateSafety(rootFolder, filesInEachFolder, subfoldersInEachFolder, nestingLevel, errorsCount, warningsCount, true);
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
      if (nestingLevel < 0)
        return;

      if (shouldInit)
        myFolderCreator.Init(rootFolder);

      for (var i = 0; i < filesInEachFolder; i++)
      {
        var filePath = rootFolder + @"\file" + i + ".cs";
        var content = myFileContentGenerator.Generate(errorsCount, warningsCount);
        myFileCreator.Create(filePath, content);
      }

      for (var i = 0; i < subfoldersInEachFolder; i++)
      {
        var folderPath = rootFolder + @"\folder" + i;
        myFolderCreator.Create(folderPath);
        GenerateSafety(folderPath, filesInEachFolder, subfoldersInEachFolder, nestingLevel - 1, errorsCount, warningsCount, false);
      }
    }
  }
}
