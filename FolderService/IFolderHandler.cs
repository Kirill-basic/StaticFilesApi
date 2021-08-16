namespace FolderService
{
    public interface IFolderHandler
    {
        public string CreateDirectory();

        public string GetCompleteFilePath(string fileName, string fileExtension);
    }
}
