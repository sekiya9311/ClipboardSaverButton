using ClipboardSaverButton.Models.Impls;
using ClipboardSaverButton.Models.Interfaces;
using Moq;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Xunit;

namespace ClipboardSaverButton.Tests.Models
{
    public class ClipboardDataSaverTest
    {
        private readonly Mock<IClipboardManager> _clipboardManager;
        private readonly Mock<IDataSaver> _dataSaver;
        private readonly Mock<IFilePathInquirer> _filePathInquirer;
        private readonly Mock<IDialogService> _dialogService;

        public ClipboardDataSaverTest()
        {
            _clipboardManager = new Mock<IClipboardManager>();
            _dataSaver = new Mock<IDataSaver>();
            _filePathInquirer = new Mock<IFilePathInquirer>();
            _dialogService = new Mock<IDialogService>();
        }

        [Fact]
        public void SaveImage()
        {
            _clipboardManager
                .SetupGet(x => x.CurrentRetenteFormat)
                .Returns(ClipboardDataFormat.Image);
            _filePathInquirer
                .Setup(x => x.InquerySaveFilePathOfImage())
                .Callback(() =>
                {
                    // Clipboard changed while selected save place
                    _clipboardManager
                        .Setup(x => x.GetImage())
                        .Returns(CreateDummyBitmap());
                })
                .Returns("path");
            var dummyImg = CreateDummyBitmap();
            _clipboardManager
                .Setup(x => x.GetImage())
                .Returns(dummyImg);

            var obj = new ClipboardDataSaver(
                _clipboardManager.Object,
                _dataSaver.Object,
                _filePathInquirer.Object,
                _dialogService.Object);
            obj.Save();

            _filePathInquirer
                .Verify(x => x.InquerySaveFilePathOfImage());
            _clipboardManager
                .Verify(x => x.GetImage());
            _dataSaver
                .Verify(x => x.SaveImage("path", dummyImg));
        }

        [Fact]
        public void SaveFile()
        {
            _clipboardManager
                .SetupGet(x => x.CurrentRetenteFormat)
                .Returns(ClipboardDataFormat.File);
            _filePathInquirer
                .Setup(x => x.InquerySaveFilePath())
                .Callback(() =>
                {
                    // Clipboard changed while selected save place
                    _clipboardManager
                        .Setup(x => x.GetFileDropList())
                        .Returns(new[] { "aiueo" });
                })
                .Returns("path");
            var dummyPaths = new[]
            {
                Path.Combine("foo", "bar"),
                Path.Combine("hoge", "fuga")
            };
            _clipboardManager
                .Setup(x => x.GetFileDropList())
                .Returns(dummyPaths);

            var obj = new ClipboardDataSaver(
                _clipboardManager.Object,
                _dataSaver.Object,
                _filePathInquirer.Object,
                _dialogService.Object);
            obj.Save();

            _filePathInquirer
                .Verify(x => x.InquerySaveFilePath());
            _clipboardManager
                .Verify(x => x.GetFileDropList());
            _dataSaver
                .Verify(x => x.SaveFiles("path", dummyPaths));
        }

        [Fact]
        public void SaveText()
        {
            _clipboardManager
                .SetupGet(x => x.CurrentRetenteFormat)
                .Returns(ClipboardDataFormat.Text);
            _filePathInquirer
                .Setup(x => x.InquerySaveFilePathOfText())
                .Callback(() =>
                {
                    // Clipboard changed while selected save place
                    _clipboardManager
                        .Setup(x => x.GetText())
                        .Returns("meow");
                })
                .Returns("path");
            _clipboardManager
                .Setup(x => x.GetText())
                .Returns("text");

            var obj = new ClipboardDataSaver(
                _clipboardManager.Object,
                _dataSaver.Object,
                _filePathInquirer.Object,
                _dialogService.Object);
            obj.Save();

            _filePathInquirer
                .Verify(x => x.InquerySaveFilePathOfText());
            _clipboardManager
                .Verify(x => x.GetText());
            _dataSaver
                .Verify(x => x.SaveText("path", "text"));
        }

        [Fact]
        public void SaveOther()
        {
            _clipboardManager
                .SetupGet(x => x.CurrentRetenteFormat)
                .Returns(ClipboardDataFormat.Other);

            var obj = new ClipboardDataSaver(
                _clipboardManager.Object,
                _dataSaver.Object,
                _filePathInquirer.Object,
                _dialogService.Object);
            obj.Save();

            _dialogService
                .Verify(x => x.ShowMessage("error", It.IsAny<string>()));
        }

        private BitmapSource CreateDummyBitmap()
        {
            var pf = PixelFormats.Pbgra32;
            int rawStride = (200 * pf.BitsPerPixel + 7) / 8;
            byte[] rawImage = new byte[rawStride * 200];
            var bitmap = BitmapSource.Create(
                200, 200,
                96, 96,
                pf, null,
                rawImage, rawStride);

            return bitmap;
        }
    }
}
