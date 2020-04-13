using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace ClipboardSaverButton.Models.Interfaces
{
    public interface IDataSaver
    {
        void SaveImage(string path, BitmapSource value);
        void SaveText(string path, string value);
    }
}
