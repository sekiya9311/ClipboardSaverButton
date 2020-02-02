using ClipboardSaverButton.Models.Interfaces;
using Microsoft.Win32;

namespace ClipboardSaverButton.Models.Impls
{
    public class FilePathInquirer : IFilePathInquirer
    {
        public string InquerySaveFilePath()
        {
            using var dialog = new System.Windows.Forms.FolderBrowserDialog();

            return dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK
                ? dialog.SelectedPath : "";
        }

        public string InquerySaveFilePathOfImage()
            => InquerySaveFilePathInternal("jpeg|*.jpg|bmp|*.bmp|png|*.png");

        public string InquerySaveFilePathOfText()
            => InquerySaveFilePathInternal("text|*.txt");

        private string InquerySaveFilePathInternal(string filter)
        {
            var dialog = new SaveFileDialog
            {
                Filter = filter
            };

            return dialog.ShowDialog().GetValueOrDefault()
                ? dialog.FileName : "";
        }
    }
}
