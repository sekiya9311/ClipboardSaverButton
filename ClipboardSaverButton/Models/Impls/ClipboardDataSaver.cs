﻿using ClipboardSaverButton.Models.Interfaces;
using System;
using System.Linq;

namespace ClipboardSaverButton.Models.Impls
{
    public class ClipboardDataSaver : IClipboardDataSaver
    {
        private readonly IClipboardManager _clipboardManager;
        private readonly IDataSaver _dataSaver;
        private readonly IFilePathInquirer _filePathInquirer;
        private readonly IDialogService _dialogService;

        public ClipboardDataSaver(
            IClipboardManager clipboardManager,
            IDataSaver dataSaver,
            IFilePathInquirer filePathInquirer,
            IDialogService dialogService)
        {
            _clipboardManager = clipboardManager;
            _dataSaver = dataSaver;
            _filePathInquirer = filePathInquirer;
            _dialogService = dialogService;
        }

        public void Save()
        {
            try
            {
                var destFiles = _clipboardManager.CurrentRetenteFormat switch
                {
                    ClipboardDataFormat.Image => new[] { SaveImage() },
                    ClipboardDataFormat.Text => new[] { SaveText() },
                    _ => Array.Empty<string>()
                };

                if (destFiles.Any())
                    return;

                _dialogService.ShowMessage("unsupported...");
            }
            catch (Exception ex)
            {
                _dialogService.ShowMessage("error", ex.Message);
            }
        }

        private string SaveImage()
        {
            var value = _clipboardManager.GetImage();

            var dest = _filePathInquirer.InquerySaveFilePathOfImage();

            _dataSaver.SaveImage(dest, value);

            return dest;
        }

        private string SaveText()
        {
            var value = _clipboardManager.GetText();

            var dest = _filePathInquirer.InquerySaveFilePathOfText();

            _dataSaver.SaveText(dest, value);

            return dest;
        }
    }
}
