using System.Windows.Media.Imaging;

namespace ClipboardSaverButton.Models.Interfaces
{
    public interface IClipboardManager
    {
        ClipboardDataFormat CurrentRetenteFormat { get; }
        string[] GetFileDropList();
        BitmapSource GetImage();
        string GetText();
    }

    public enum ClipboardDataFormat
    {
        Image,
        File,
        Text,
        Other
    }
}
