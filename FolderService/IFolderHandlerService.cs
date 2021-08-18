namespace FolderService
{
    public interface IFolderHandlerService
    {
        string GetCompleteFilePath(string fileName, string fileExtension);
        string GetFileSubFolder(string fileName, string fileExtension);
    }
}