using ClipboardSaverButton.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;

namespace ClipboardSaverButton.Models.Impls
{
    public class DataSaver : IDataSaver
    {
        public void SaveFiles(string path, IEnumerable<string> values)
        {
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException();

            foreach (var srcFile in values)
            {
                // 無いやつ、ディレクトリは除外(めんどいから)
                if (!File.Exists(srcFile)) continue;

                var fileName = Path.GetFileName(srcFile);
                var dest = Path.Combine(path, fileName);
                File.Copy(srcFile, dest);
            }
        }

        public void SaveImage(string path, BitmapSource value)
        {
            BitmapEncoder encoder = Path.GetExtension(path) switch
            {
                ".jpg" => new JpegBitmapEncoder(),
                ".jpeg" => new JpegBitmapEncoder(),
                ".png" => new PngBitmapEncoder(),
                ".bmp" => new BmpBitmapEncoder(),
                _ => throw new ArgumentException("Not compatible format type.")
            };
            encoder.Frames.Add(BitmapFrame.Create(value));

            using var stream = new FileStream(path, FileMode.Create);
            encoder.Save(stream);
        }

        public void SaveText(string? path, string value)
        {
            File.WriteAllText(path, value);
        }
    }
}
