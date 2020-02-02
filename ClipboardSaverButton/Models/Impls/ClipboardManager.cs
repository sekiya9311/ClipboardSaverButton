using ClipboardSaverButton.Models.Interfaces;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ClipboardSaverButton.Models.Impls
{
    public class ClipboardManager : IClipboardManager
    {
        public ClipboardDataFormat CurrentRetenteFormat
        {
            get
            {
                if (Clipboard.ContainsImage())
                    return ClipboardDataFormat.Image;

                if (Clipboard.ContainsText())
                    return ClipboardDataFormat.Text;

                if (Clipboard.ContainsFileDropList())
                    return ClipboardDataFormat.File;

                return ClipboardDataFormat.Other;
            }
        }

        public string[] GetFileDropList()
        {
            CheckCurrent(ClipboardDataFormat.File);

            return Clipboard.GetFileDropList()
                .OfType<string>()
                .ToArray();
        }

        public BitmapSource GetImage()
        {
            CheckCurrent(ClipboardDataFormat.Image);

            return Clipboard.GetImage();
        }

        public string GetText()
        {
            CheckCurrent(ClipboardDataFormat.Text);

            return Clipboard.GetText();
        }

        private void CheckCurrent(ClipboardDataFormat need)
        {
            if (CurrentRetenteFormat != need)
                throw new InvalidOperationException($"Current data format is {need}");
        }
    }
}
