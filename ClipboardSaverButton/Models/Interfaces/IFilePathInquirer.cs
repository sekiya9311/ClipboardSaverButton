namespace ClipboardSaverButton.Models.Interfaces
{
    public interface IFilePathInquirer
    {
        string InquerySaveFilePath();
        string InquerySaveFilePathOfImage();
        string InquerySaveFilePathOfText();
    }
}
